using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    [DataContract]
    public class App : TableEntity
    {
        [DataMember]
        public string Id
        {
            get
            {
                return this.RowKey;
                
            }

            set
            {
                this.RowKey = value;
            }
        }

        [DataMember]
        public string DeviceId
        {
            get
            {
                return this.PartitionKey;
                
            }

            set
            {
                this.PartitionKey = value;
                
            }
        }
    }
}