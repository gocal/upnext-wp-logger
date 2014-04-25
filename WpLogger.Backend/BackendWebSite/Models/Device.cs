using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    public class Device : TableEntity
    {
        private string _id;
        
        public Device()
        {
            this.PartitionKey = "Devices";
        }

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