// Type: eBay.Service.Core.Sdk.ApiCall
// Assembly: eBay.Service, Version=3.787.0.0, Culture=neutral, PublicKeyToken=1d9d786a5932eaf0
// Assembly location: C:\Users\Alex\Documents\Visual Studio 2010\Projects\Ebay1\eBay.Service.dll

using eBay.Service.Core.Soap;
using eBay.Service.Util;
using System;
using System.Runtime.InteropServices;

namespace eBay.Service.Core.Sdk
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class ApiCall
    {
        public ApiCall();
        public ApiCall(ApiContext ApiContext);
        public AbstractResponseType ExecuteRequest(AbstractRequestType Request);
        public virtual void Execute();
        protected virtual CustomSecurityHeaderType GetSecurityHeader();
        protected internal void LogMessage(string Message, MessageType Type, MessageSeverity Severity);
        protected internal void LogMessagePayload(string Message, MessageSeverity Severity, Exception Ex);
        public Type InterfaceServiceType { get; set; }
        public ApiException ApiException { get; }
        public bool HasError { get; }
        public bool HasWarning { get; }
        public ApiContext ApiContext { get; set; }
        public AbstractRequestType AbstractRequest { get; set; }
        public AbstractResponseType AbstractResponse { get; }
        public CallRetry CallRetry { get; set; }
        public DetailLevelCodeTypeCollection DetailLevelList { get; set; }
        protected internal bool DetailLevelOverride { get; set; }
        public int Timeout { get; set; }
        public string Version { get; set; }
        public SiteCodeType Site { get; set; }
        public TimeSpan ResponseTime { get; }
        public string SoapRequest { get; }
        public string SoapResponse { get; }
        public bool EnableCompression { get; set; }
        public CallMetricsEntry CallMetricsEntry { get; set; }
        public event BeforeRequestEventHandler BeforeRequest;
        public event AfterRequestEventHandler AfterRequest;
    }
}
