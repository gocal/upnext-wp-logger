using System;
using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    [DataContract]
    public class LogEntry : TableEntity
    {
        private string _deviceId;
        public string DeviceId
        {
            get { return _deviceId; }
            set
            {
                _deviceId = value;
                UpdatePartitionKey();
            }
        }

        private string _appId;

        public string AppId
        {
            get { return _appId; }
            set
            {
                _appId = value;
                UpdatePartitionKey();
            }
        }

        [DataMember]
        public string LogLevel { get; set; }

        [DataMember]
        public string Tag { get; set; }

        [DataMember]
        public string Content { get; set; }

        private DateTime _timeStamp;
        [DataMember]
        public DateTime TimeStamp {
            get { return _timeStamp; }
            set
            {
                _timeStamp = value;
                this.RowKey = value.ToString("O");
            } }


        private void UpdatePartitionKey()
        {
            this.PartitionKey = (this.DeviceId ?? string.Empty) + "_____" + (this.AppId ?? string.Empty);
        }
    }
}