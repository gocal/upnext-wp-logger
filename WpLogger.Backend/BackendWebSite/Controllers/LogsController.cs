using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BackendWebSite.Models;

namespace BackendWebSite.Controllers
{
    public class LogsController : ApiController
    {
        IDevicesRepository devicesRepository = new DevicesRepository();
        IAppRepository appRepository = new AppRepository();
        ILogRepository logRepository = new LogRepository();

        public async Task<IEnumerable<LogEntry>> GetAllByDeviceIdAndAppId([FromUri]string deviceId, [FromUri]string appId)
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

        public void Post([FromBody]LogEntry logEntry, [FromUri]string deviceId, [FromUri]string appId)
        {
            var device = new Device {Id = deviceId};
            var app = new App {DeviceId = deviceId, Id = appId};

            devicesRepository.AddDevice(device);

        }
    }
}
