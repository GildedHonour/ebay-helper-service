using System;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;

namespace Ebay1
{
    public partial class Listings
    {
        public bool IsApiCallSuccess { get; set; }
        public string RelistedItemID { get; set; }
        public string ApiCallErrorMessage { get; set; }
        public ApiCallTypes ApiCallType { get; set; }

        private ItemType ToItemType()
        {
            ItemType itemType = new ItemType();
            AmountType amount = new AmountType();
            amount.Value = (double)SellingPrice;
            amount.currencyID = CurrencyCodeType.USD;
            itemType.StartPrice = amount;
            itemType.ItemID = ListingID;
            itemType.Quantity = Quantity;
            return itemType;
        }

        public void Revise(ApiContext apiContext, System.Diagnostics.EventLog eventLog = null)
        {
            ItemType itemType = ToItemType();
            ReviseItemCall apiCall = new ReviseItemCall(apiContext);
            if (eventLog != null)
            {
                eventLog.WriteEntry(string.Format("Start revising an item... ItemID={0}, Quantity={1}, Price={2}", itemType.ItemID, itemType.Quantity, itemType.StartPrice.Value));
            }

            try
            {
                apiCall.ReviseItem(itemType, null, false);
                IsApiCallSuccess = true;
            }
            catch (Exception e)
            {
                if (eventLog != null)
                {
                    eventLog.WriteEntry(string.Format("An error occured while revising an item... ItemID={0}, Quantity={1}, Price={2}, Message={3}", itemType.ItemID, itemType.Quantity, itemType.StartPrice.Value, e.Message));
                }

                IsApiCallSuccess = false;
                ApiCallErrorMessage = e.Message;
            }

            Console.WriteLine(string.Format("item was updated, id={0}, result={1}", ListingID, IsApiCallSuccess));
        }

        public void Relist(ApiContext apiContext, System.Diagnostics.EventLog eventLog = null)
        {
            ItemType itemType = ToItemType();
            RelistItemCall apiCall = new RelistItemCall(apiContext);
            if (eventLog != null)
            {
                eventLog.WriteEntry(string.Format("Start relisting an item... ItemID={0}, Quantity={1}, Price={2}", itemType.ItemID, itemType.Quantity, itemType.StartPrice.Value));
            }

            try
            {
                apiCall.RelistItem(itemType);
                RelistedItemID = itemType.ItemID;
                IsApiCallSuccess = true;
            }
            catch (Exception e)
            {
                if (eventLog != null)
                {
                    eventLog.WriteEntry(string.Format("An error occured while relisting an item... ItemID={0}, Quantity={1}, Price={2}, Message={3}", itemType.ItemID, itemType.Quantity, itemType.StartPrice.Value, e.Message));
                }

                IsApiCallSuccess = false;
                ApiCallErrorMessage = e.Message;
            }

            Console.WriteLine("item was relisted, result={0}", IsApiCallSuccess);
        }
    }

    public enum ApiCallTypes
    {
        Updating,
        Relisting,
        Adding
    }
}
