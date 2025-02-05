using DealershipApp.Models;

namespace DealershipApp.Interfaces
{
    public interface IServiceRepository
    {
        ICollection<Service> GetAllServices();
        DateTime RepairDateById(int serviceId);
        string GetDescriptionById(int serviceId);
        bool ServiceExists(int serviceId);
        bool CreateService(Service service);
        bool Save();
    }
}
