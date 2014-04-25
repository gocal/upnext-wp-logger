using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using BackendWebSite.Models;

namespace BackendWebSite.Controllers
{
    
    public class DevicesController : ApiController
    {
        private static readonly IDevicesRepository repo = new DevicesRepository();

        // GET api/devices
        public async Task<IEnumerable<string>> GetAll()
        {
            return (await repo.GetDevices()).Select(dev => dev.Id);
        }

        public async Task<Device> GetByDeviceId([FromUri]string deviceId)
        {
            if (deviceId.Length > 1000)
            {
                return null;
            }
            var result = await repo.GetDevice(deviceId);
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return result;
        }
    }
}
