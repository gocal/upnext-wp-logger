using System;
using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    [DataContract]
    public class LogEntry : TableEntity
    {
        public string DeviceId
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string AppId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

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