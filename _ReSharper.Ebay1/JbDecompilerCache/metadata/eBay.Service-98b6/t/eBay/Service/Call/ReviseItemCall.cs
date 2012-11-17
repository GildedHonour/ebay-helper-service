// Type: eBay.Service.Call.ReviseItemCall
// Assembly: eBay.Service, Version=3.787.0.0, Culture=neutral, PublicKeyToken=1d9d786a5932eaf0
// Assembly location: C:\Users\Alex\Documents\Visual Studio 2010\Projects\Ebay1\eBay.Service.dll

using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using System;
using System.Runtime.InteropServices;

namespace eBay.Service.Call
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class ReviseItemCall : ApiCall
    {
        public ReviseItemCall();
        public ReviseItemCall(ApiContext ApiContext);
        public DateTime ReviseItem(ItemType Item, StringCollection DeletedFieldList, bool VerifyOnly);
        public override void Execute();
        public ApiCall ApiCallBase { get; }
        public ReviseItemRequestType ApiRequest { get; set; }
        public ReviseItemResponseType ApiResponse { get; }
        public ItemType Item { get; set; }
        public StringCollection DeletedFieldList { get; set; }
        public bool VerifyOnly { get; set; }
        public StringCollection PictureFileList { get; set; }
        public string ItemID { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public FeeTypeCollection FeeList { get; }
        public string CategoryID { get; }
        public string Category2ID { get; }
        public bool VerifyOnlyReturn { get; }
        public DiscountReasonCodeTypeCollection DiscountReasonList { get; }
        public ProductSuggestionsType ProductSuggestions { get; }
    }
}
