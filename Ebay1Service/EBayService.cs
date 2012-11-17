using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using eBay.Service.Call;
using System.Data.Objects.DataClasses;
using System.Transactions;


namespace Ebay1Service
{
    public partial class EBayService : ServiceBase
    {
        private static ApiContext apiContext;
        private static readonly Regex regexWhiteSpacesTrimmer = new Regex(@"\s+");
        private static Timer timer;
        private static readonly int dueTime = int.Parse(ConfigurationManager.AppSettings["dueTime"]) * 1000;
        private static List<Listings> itemsList = new List<Listings>();
        public const string ServicePrettyName = "eBayHelperService";

        /// <summary>
        /// Occurs when service is starting
        /// </summary>
        /// <param name="args">Command line arguments</param>
        protected override void OnStart(string[] args)
        {
            apiContext = GetApiContext();
            timer = new Timer(_ => OnTimer(), null, dueTime, Timeout.Infinite);
        }

        /// <summary>
        /// Callback method for timer
        /// </summary>
        private static void OnTimer()
        {
            using (var context = new EbayDb { CommandTimeout = 120 })
            {
                var items = context.Listings.Where(x => !x.IsUsed);
                if (items.Any())
                {
                    #region
                    foreach (var item in items.ToList())
                    {
                        GetItemCall apiCall = new GetItemCall(apiContext);
                        if (item.Quantity <= 0)
                        {
                            item.ApiCallType = ApiCallTypes.Ending;
                            item.End(apiContext);
                        }
                        else
                        {
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
                        }

                        itemsList.Add(item);
                        if (item.IsApiCallSuccess)
                        {
                            if (item.ApiCallType == ApiCallTypes.Updating || item.ApiCallType == ApiCallTypes.Ending)
                            {
                                item.IsUsed = true;
                                context.SaveChanges();
                            }
                            else if (item.ApiCallType == ApiCallTypes.Relisting)
                            {
                                using (var scope = new TransactionScope())
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

        /// <summary>
        /// Restarts timer
        /// </summary>
        private static void RestartTimer()
        {
            timer.Change(dueTime, 0);
        }

        /// <summary>
        /// Generates file name for resulting xml file
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Creates resulting xml file
        /// </summary>
        /// <param name="listings"></param>
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
        }

        /// <summary>
        /// Returns api context
        /// </summary>
        /// <returns>Instance of ApiContext</returns>
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
