using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    public class DevicesRepository : IDevicesRepository
    {
        public async Task AddDevice(Device device)
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.InsertOrReplace(device);

            await tableReference.ExecuteAsync(insertOperation);
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            var query = new TableQuery<Device>();
            TableContinuationToken continuationToken = null;
            var cancellationToken = new CancellationToken();

            return await tableReference.ExecuteQuerySegmentedAsync(query, continuationToken, cancellationToken);
        }

        public async Task<Device> GetDevice(string id)
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            var query = new TableQuery<Device>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));
            TableContinuationToken continuationToken = null;
            var cancellationToken = new CancellationToken();

            return (await tableReference.ExecuteQuerySegmentedAsync(query, continuationToken, cancellationToken)).FirstOrDefault();
        }

        private CloudTable GetTableReference()
        {
            return CloudStorageAccess.GetTableClient().GetTableReference("Devices");
        }
    }
}