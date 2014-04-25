using System;

namespace WpLogger.DataContract.Model
{
    public class LogEntry
    {
        public string DeviceId { get; set; }

        public string AppId { get; set; }

        public string Tag { get; set; }

        public string Content { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
