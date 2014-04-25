using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendWebSite.Models
{
    public interface IAppRepository
    {
        Task AddApp(App app);

        Task<App> GetApp(string id, string deviceId);

        Task<IEnumerable<App>> GetApps(string deviceId);
    }
}
