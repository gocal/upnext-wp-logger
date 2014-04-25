using System.Collections.Generic;
using System.Web.Http;

namespace BackendWebSite.Controllers
{
    public class LogsController : ApiController
    {
        // GET: api/Logs
        public IEnumerable<string> Get()
        {
            return new string[] { "log1", "log2" };
        }

        // POST: api/Logs
        public void Post([FromBody]string value)
        {
        }
    }
}
