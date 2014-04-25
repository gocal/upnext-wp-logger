using System.Collections.Generic;
using System.Web.Http;

namespace BackendWebSite.Controllers
{
    public class AppsController : ApiController
    {
        // GET: api/Apps
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Apps/5
        public string GetByAppId([FromUri]string appId)
        {
            return "value";
        }
    }
}
