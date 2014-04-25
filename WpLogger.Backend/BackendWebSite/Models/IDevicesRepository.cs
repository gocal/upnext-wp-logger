using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendWebSite.Models
{
    public interface IDevicesRepository
    {
        Task AddDevice(Device device);

        Task<Device> GetDevice(string id);

        Task<IEnumerable<Device>> GetDevices();
    }
}
