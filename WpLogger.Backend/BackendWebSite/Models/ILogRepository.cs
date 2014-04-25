using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendWebSite.Models
{
    public interface ILogRepository
    {
        Task SaveLogEntry(LogEntry logEntry);

        Task<IEnumerable<LogEntry>> GetLogEntries(string deviceId, string appId, DateTime? from, DateTime? to);
    }
}
