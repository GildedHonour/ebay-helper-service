// Type: eBay.Service.Util.FileLogger
// Assembly: eBay.Service, Version=3.787.0.0, Culture=neutral, PublicKeyToken=1d9d786a5932eaf0
// Assembly location: C:\Users\Alex\Documents\Visual Studio 2010\Projects\Ebay1\eBay.Service.dll

namespace eBay.Service.Util
{
    public class FileLogger : ApiLogger
    {
        public FileLogger();
        public FileLogger(string FileName);
        public FileLogger(string FileName, bool LogInformations, bool LogApiMessages, bool LogExceptions);
        public override void RecordMessage(string Message, MessageSeverity Severity);
        public void Close();
        public bool isAbsolutePath(string path);
        public string FileName { get; set; }
    }
}
