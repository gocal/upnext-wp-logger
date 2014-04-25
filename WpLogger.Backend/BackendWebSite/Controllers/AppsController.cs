using System.Collections.Generic;
using System.Web.Http;

namespace BackendWebSite.Controllers
{
    public class AppsController : ApiController
    {
        // GET: api/Apps
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Apps/5
        public string Get(string id)
        {
            return "value";
        }
    }
}
