using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BackendWebSite.Models;

namespace BackendWebSite.Controllers
{
    
    public class DevicesController : ApiController
    {
        private static readonly IDevicesRepository repo = new DevicesRepository();

        // GET api/devices
        public async Task<IEnumerable<string>> Get()
        {
            return (await repo.GetDevices()).Select(dev => dev.Id);
        }

        public async Task<Device> Get(string deviceId)
        {
            if (deviceId.Length > 1000)
            {
                return null;
            }
            return await repo.GetDevice(deviceId);
        }

        /*
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
         * */
    }
}
