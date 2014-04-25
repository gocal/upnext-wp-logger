using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace BackendWebSite.Models
{
    public class LogRepository : ILogRepository
    {
        public Task SaveLogEntry(LogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LogEntry> GetLogEntries(string deviceId, string appId, DateTime? @from)
        {
            throw new NotImplementedException();
        }

        private CloudTable GetTableReference()
        {
            return CloudStorageAccess.GetTableClient().GetTableReference("WpLogs");
        }
    }
}