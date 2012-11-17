using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using System.Transactions;
using eBay.Service.Call;
using eBay.Service.Util;

namespace Ebay1
{

    public partial class Program
    {
        private static ApiContext apiContext;
        private static readonly Regex regexWhiteSpacesTrimmer = new Regex(@"\s+");
        private static Timer timer;
        private static readonly int dueTime = int.Parse(ConfigurationManager.AppSettings["dueTime"]) * 1000;
        private static List<Listings> itemsList = new List<Listings>();

        private static void Main()
        {
            apiContext = GetApiContext();
            timer = new Timer(_ => OnTimer(), null, dueTime, Timeout.Infinite);
            Console.ReadLine();
        }

        private static void OnTimer()
        {
            using (var context = new EbayDb())
            {
                var items = context.Listings.Where(x => !x.IsUsed);
                if (items.Any())
                {
                    #region
                    foreach (var item in items.ToList())
                    {
                        GetItemCall apiCall = new GetItemCall(apiContext);
                        var status = apiCall.GetItem(item.ListingID).SellingStatus.ListingStatus;
                        if (status == ListingStatusCodeType.Active)
                        {
                            item.ApiCallType = ApiCallTypes.Updating;
                            item.Revise(apiContext);
                        }
                        else if (status == ListingStatusCodeType.Completed || status == ListingStatusCodeType.Ended)
                        {
                            item.ApiCallType = ApiCallTypes.Relisting;
                            item.Relist(apiContext);
                        }

                        itemsList.Add(item);
                        if (item.IsApiCallSuccess)
                        {
                            if (item.ApiCallType == ApiCallTypes.Updating)
                            {
                                item.IsUsed = true;
                                context.SaveChanges();
                            }
                            else if (item.ApiCallType == ApiCallTypes.Relisting)
                            {
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    context.DeleteObject(item);
                                    var newItem = new Listings
                                    {
                                        ListingID = item.RelistedItemID,
                                        RelistedItemID = item.ListingID,
                                        MPN = item.MPN,
                                        SellingPrice = item.SellingPrice,
                                        Quantity = item.Quantity,
                                        IsUsed = true
                                    };
                                    context.Listings.AddObject(newItem);
                                    context.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
                                    scope.Complete();
                                    context.AcceptAllChanges();
                                }
                            }
                        }
                    }
                    #endregion

                    CreateXmlFile(itemsList);
                }
            }

            itemsList.Clear();
            RestartTimer();
        }

        private static void RestartTimer()
        {
            timer.Change(dueTime, 0);
        }

        private static string GenerateXmlFileName()
        {
            string uniqueFileName = string.Format("{0}-{1}-{2}__{3}-{4}-{5}",
                                                  DateTime.Now.Year, DateTime.Now.Month,
                                                  DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute,
                                                  DateTime.Now.Second);
            uniqueFileName += ".xml";
            string donePath = ConfigurationManager.AppSettings["donePath"];
            string fullFileName;
            if (!donePath.Contains(":"))
            {
                fullFileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, donePath, uniqueFileName);
            }
            else
            {
                fullFileName = Path.Combine(donePath, uniqueFileName);
            }

            return fullFileName;
        }

        private static void CreateXmlFile(IEnumerable<Listings> listings)
        {
            using (var stream = new FileStream(GenerateXmlFileName(), FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (XmlWriter writter = new XmlTextWriter(stream, null))
                {
                    writter.WriteStartDocument();
                    writter.WriteStartElement("items");
                    writter.WriteAttributeString("dateTime", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    writter.WriteAttributeString("total", listings.Count().ToString(CultureInfo.InvariantCulture));
                    writter.WriteAttributeString("success", listings.Count(x => x.IsApiCallSuccess).ToString(CultureInfo.InvariantCulture));
                    writter.WriteAttributeString("fail", listings.Count(x => !x.IsApiCallSuccess).ToString(CultureInfo.InvariantCulture));

                    foreach (var listingIttem in listings)
                    {
                        writter.WriteStartElement("item");
                        writter.WriteElementString("id", listingIttem.ListingID);
                        writter.WriteElementString("quantity", listingIttem.Quantity.ToString(CultureInfo.InvariantCulture));
                        writter.WriteElementString("price", listingIttem.SellingPrice.ToString(CultureInfo.InvariantCulture));
                        writter.WriteElementString("result", listingIttem.IsApiCallSuccess ? "success" : "fail");
                        writter.WriteElementString("operation", listingIttem.ApiCallType.ToString());
                        writter.WriteElementString("previousID", listingIttem.ApiCallType == ApiCallTypes.Relisting ? listingIttem.RelistedItemID : "-1");
                        writter.WriteElementString("errorMessage", listingIttem.ApiCallErrorMessage ?? "no message");
                        writter.WriteEndElement();
                    }

                    writter.WriteEndElement();
                    writter.WriteEndDocument();
                }
            }

            Console.WriteLine("XML file was created");
        }

        private static ApiContext GetApiContext()
        {
            if (apiContext != null)
            {
                return apiContext;
            }
            else
            {
                apiContext = new ApiContext();
                apiContext.SoapApiServerUrl = ConfigurationManager.AppSettings["environment.ApiServerUrl"];
                ApiCredential apiCredential = new ApiCredential();
                apiCredential.eBayToken = ConfigurationManager.AppSettings["userAccount.ApiToken"];
                apiContext.ApiCredential = apiCredential;
                apiContext.Site = SiteCodeType.US;
                bool result;
                if (bool.TryParse(ConfigurationManager.AppSettings["enableLogging"], out result) && result)
                {
                    apiContext.ApiLogManager = new ApiLogManager();
                    string logFullFileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "log.txt");
                    apiContext.ApiLogManager.ApiLoggerList.Add(new FileLogger(logFullFileName, true, true, true));
                    apiContext.ApiLogManager.EnableLogging = true;
                }

                return apiContext;
            }
        }
    }
}
