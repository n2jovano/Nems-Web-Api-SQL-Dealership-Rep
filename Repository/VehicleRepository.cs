using DealershipApp.Data;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DealershipApp.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext _context;

        public VehicleRepository(DataContext context)
        {
            _context = context;
        }

        public Vehicle GetVehicle(int id)
        {
            return _context.Vehicles.Where(p => p.Id == id).FirstOrDefault();
        }

        public decimal GetVehicleAvgYear()
        {
            var count = _context.Vehicles.Where(p => p.VehicleYear > 1990);
            if (count.Count() <= 0)
            {
                return 0;
            }
            else
            {
                return ((decimal)count.Sum(p => p.VehicleYear) / count.Count());            
            }
        }

        public string GetVehicleColourById(int vehicleId)//work here
        {
            return _context.Vehicles.Where(p => p.Id == vehicleId).Select(c => c.Colour).FirstOrDefault();
        }

        public ICollection<Vehicle> GetAllVehiclesByColour(string colour)
        {
            return _context.Vehicles.Where(p => p.Colour.ToLower() == colour.ToLower()).ToList();
        }

        public ICollection<Vehicle> GetVehicles()
        {
            return _context.Vehicles.OrderBy(p => p.Id).ToList();
        }
        public bool GetVehicleIsAutomatic(int vehicleId)
        {
            return _context.Vehicles.Where(p => p.Id == vehicleId).Select(c => c.IsAutomatic).FirstOrDefault();
        }

        public int GetVehicleYear(int vehicleId)
        {
            //return _context.Vehicles.Where(p => p.VehicleYear == year).FirstOrDefault();
            return _context.Vehicles.Where(p => p.Id == vehicleId).Select(c => c.VehicleYear).FirstOrDefault();
        }

        public bool VehicleExists(int vehicleId)
        {
            return _context.Vehicles.Any(p => p.Id == vehicleId);
        }

        public DateTime GetVehicleOrderDate(int id)
        {
            return _context.Vehicles.Where(p => p.Id == id).Select(c => c.OrderDate).FirstOrDefault();
        }

        public bool CreateVehicle(Vehicle vehicle)
        {
            //EntityState @ 5:32 9
            //going to be tracking entity
            //updating, adding, removing
            //dbcontext going to be doing all tracking
            _context.Add(vehicle);

            return Save();
        }

        public bool Save()
        {
            //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Vehicles] ON");
            var stateSaved = _context.SaveChanges();  //sql generated and sent to db

            //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Vehicles] OFF");
            return stateSaved > 0 ? true : false;
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            _context.Update(vehicle);

            return Save();
        }

        public bool DeleteVehicle(Vehicle vehicle)
        {
            _context.Remove(vehicle);
            return Save();
        }
    }
}
