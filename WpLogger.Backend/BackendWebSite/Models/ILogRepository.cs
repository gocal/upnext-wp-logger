using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendWebSite.Models
{
    public interface ILogRepository
    {
        Task SaveLogEntry(LogEntry logEntry);

        IEnumerable<LogEntry> GetLogEntries(string deviceId, string appId, DateTime? from);
    }
}
