using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<LogEntry>> GetLogEntries(string deviceId, string appId, DateTimeOffset? @from, DateTimeOffset? to)
        {
            var tableReference = this.GetTableReference();
            await tableReference.CreateIfNotExistsAsync();

            var query = new TableQuery<LogEntry>().Where(TableQuery.GenerateFilterCondition("AppId", QueryComparisons.Equal, appId))
                .Where(TableQuery.GenerateFilterCondition("DeviceId", QueryComparisons.Equal, deviceId));

            if (from != null)
            {
                query = query.Where(TableQuery.GenerateFilterConditionForDate("TimeStamp", QueryComparisons.GreaterThanOrEqual, from.Value));
            }
            if (to != null)
            {
                query = query.Where(TableQuery.GenerateFilterConditionForDate("TimeStamp", QueryComparisons.LessThanOrEqual, to.Value));
            }

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