using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    [DataContract]
    public class Device : TableEntity
    {
        public Device()
        {
            this.PartitionKey = "Devices";
        }

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
    }
}