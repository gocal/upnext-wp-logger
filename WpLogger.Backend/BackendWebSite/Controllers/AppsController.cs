using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using BackendWebSite.Models;

namespace BackendWebSite.Controllers
{
    public class AppsController : ApiController
    {
        private static readonly IAppRepository repo = new AppRepository();

        public async Task<IEnumerable<string>> GetByDeviceId([FromUri]string deviceId)
        {
            return (await repo.GetApps(deviceId)).Select(dev => dev.Id);
        }

        // GET: api/Apps/5
        public async Task<App> GetByDeviceIdAndAppId([FromUri]string appId, [FromUri]string deviceId)
        {
            var result = await repo.GetApp(appId, deviceId);
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return result;
        }
    }
}
