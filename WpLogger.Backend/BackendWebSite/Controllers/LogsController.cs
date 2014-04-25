using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BackendWebSite.Models;

namespace BackendWebSite.Controllers
{
    public class LogsController : ApiController
    {
        // GET: api/Logs
        public async Task<IEnumerable<LogEntry>> GetAll()
        {
            var results = new List<LogEntry>();
            results.Add(new LogEntry
            {
                AppId = "123",
                Content = "LogContent",
                DeviceId = "DeviceId",
                ETag = "ETag",
                LogLevel = "LogLevel",
                PartitionKey = "PartitionKey",
                RowKey = "RowKey",
                Tag = "Tag",
                TimeStamp = DateTime.Now
            });

            return await Task.FromResult(results);
        }

        // POST: api/Logs
        public void Post([FromBody]string value)
        {
        }
    }
}
