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

        [HttpGet]
        [ActionName("LogWithDates")]
        public async Task<IEnumerable<LogEntry>> GetAllByDeviceIdAndAppId([FromUri]string deviceId, [FromUri]string appId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return await logRepository.GetLogEntries(deviceId, appId, from, to);
        }

        [HttpPost]
        [ActionName("LogWithDates")]
        public async Task Post([FromBody]LogEntry logEntry, [FromUri]string deviceId, [FromUri]string appId)
        {
            var device = new Device {Id = deviceId};
            var app = new App {DeviceId = deviceId, Id = appId};

            await devicesRepository.AddDevice(device); // in case they're not there yet
            await appRepository.AddApp(app);

            logEntry.AppId = appId;
            logEntry.DeviceId = deviceId;
            await logRepository.SaveLogEntry(logEntry);
        }
    }
}
