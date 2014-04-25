using System.Collections.Generic;
using System.Web.Http;
using BackendWebSite.Models;

namespace BackendWebSite.Controllers
{
    public class AppsController : ApiController
    {
        // GET: api/Apps
        public IEnumerable<string> GetAll()
        {
            return new string[] { "AppId", "AppId2" };
        }

        // GET: api/Apps/5
        public App GetByAppId([FromUri]string appId)
        {
            return new App {Id = "AppId"};
        }
    }
}
