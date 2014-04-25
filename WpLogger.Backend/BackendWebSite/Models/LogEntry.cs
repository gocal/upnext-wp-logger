using System;
using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    [DataContract]
    public class LogEntry : TableEntity
    {
        public string DeviceId { get; set; }

        public string AppId { get; set; }

        [DataMember]
        public string LogLevel { get; set; }

        [DataMember]
        public string Tag { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime TimeStamp { get; set; }
    }
}