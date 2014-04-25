using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    [DataContract]
    public class App : TableEntity
    {
        private string _id;
        
        public App()
        {
            this.PartitionKey = "Apps";
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