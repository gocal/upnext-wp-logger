using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    public static class CloudStorageAccess
    {
        public const string ConnectionStringName = "LogsTableStorage";

        public static CloudTableClient GetTableClient()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            return storageAccount.CreateCloudTableClient();
        }
    }
}