using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    public class AppRepository : IAppRepository
    {
        public async Task AddApp(App app)
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.InsertOrReplace(app);

            await tableReference.ExecuteAsync(insertOperation);
        }

        public async Task<App> GetApp(string id, string deviceId)
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            var query = new TableQuery<App>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id))
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, deviceId));
            TableContinuationToken continuationToken = null;
            var cancellationToken = new CancellationToken();

            return (await tableReference.ExecuteQuerySegmentedAsync(query, continuationToken, cancellationToken)).FirstOrDefault();
        }

        public async Task<IEnumerable<App>> GetApps(string deviceId)
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            var query = new TableQuery<App>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, deviceId));
            TableContinuationToken continuationToken = null;
            var cancellationToken = new CancellationToken();

            return (await tableReference.ExecuteQuerySegmentedAsync(query, continuationToken, cancellationToken));
        }

        private CloudTable GetTableReference()
        {
            return CloudStorageAccess.GetTableClient().GetTableReference("Apps");
        }
    }
}