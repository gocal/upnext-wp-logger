using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    public class LogRepository : ILogRepository
    {
        public async Task SaveLogEntry(LogEntry logEntry)
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.InsertOrReplace(logEntry);

            await tableReference.ExecuteAsync(insertOperation);
        }

        public async Task<IEnumerable<LogEntry>> GetLogEntries(string deviceId, string appId, DateTime? @from, DateTime? to)
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            var query = new TableQuery<LogEntry>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, appId))
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, deviceId));

            /*if (from != null)
            {
                query = query.Where(TableQuery.GenerateFilterCondition("TimeStamp", QueryComparisons.GreaterThanOrEqual, from.Value.ToString(CultureInfo.InvariantCulture)));
            }
            if (to != null)
            {
                query = query.Where(TableQuery.GenerateFilterCondition("TimeStamp", QueryComparisons.LessThanOrEqual, to.Value.ToString(CultureInfo.InvariantCulture)));
            }*/

            TableContinuationToken continuationToken = null;
            var cancellationToken = new CancellationToken();

            return (await tableReference.ExecuteQuerySegmentedAsync(query, continuationToken, cancellationToken));
        }

        private CloudTable GetTableReference()
        {
            return CloudStorageAccess.GetTableClient().GetTableReference("WpLogs");
        }
    }
}