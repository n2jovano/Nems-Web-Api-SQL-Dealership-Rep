using DealershipApp.Data;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using System.Diagnostics.Metrics;

namespace DealershipApp.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext _context;

        public ServiceRepository(DataContext context)
        {
            _context = context;         
        }

        public bool CreateService(Service service)
        {
            _context.Add(service);

            return Save();
        }

        public ICollection<Service> GetAllServices()
        {
            return _context.Services.OrderBy(s => s.Id).ToList();
        }

        public string GetDescriptionById(int serviceId)
        {
            return _context.Services.Where(s => s.Id == serviceId).Select(p => p.Description).FirstOrDefault();
        }

        public DateTime RepairDateById(int serviceId)
        {
            return _context.Services.Where(s => s.Id == serviceId).Select(p => p.RepairDate).FirstOrDefault();
        }

        public bool Save()
        {
            var stateSaved = _context.SaveChanges();

            return stateSaved > 0 ? true : false;
        }

        public bool ServiceExists(int serviceId)
        {
            return _context.Services.Any(p => p.Id == serviceId);
        }
    }
}
