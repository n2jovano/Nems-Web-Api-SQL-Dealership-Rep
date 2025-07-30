using DealershipApp.Data;
using DealershipApp.Dto;
using DealershipApp.Interfaces;
using DealershipApp.Models;

namespace DealershipApp.Repository
{
    public class DealershipRepository : IDealershipRepository  //(b) design
    {
        private readonly DataContext _context;
        public DealershipRepository(DataContext context)    //bring in argument of the datacontext we created previously DataContext.cs
        { 
            _context = context;
        }

        public Dealership GetDealership(int id)
        {
            return _context.Dealerships.Where(p => p.Id == id).FirstOrDefault(); //Id is the column in sql
        }

        public Dealership GetDealership(string name)
        {
            return _context.Dealerships.Where(p => p.Name == name).FirstOrDefault();
        }

        public Dealership GetDealership(bool isShowroom)
        {
            return _context.Dealerships.Where(p => p.IsShowRoom == isShowroom).FirstOrDefault();
        }

        //IDealershipInterface
        public ICollection<Dealership> GetDealerships() 
        {
            return _context.Dealerships.OrderBy(p => p.Id).ToList();         //have to be explicit by adding i.e. OrderBy(p => p.Id).ToList()
        }

        public bool DealershipExists(int dealerId)
        {
            return _context.Dealerships.Any(p => p.Id == dealerId);
        }
        //---------
        public ICollection<Vehicle> GetVehiclesByDealership(int dealerId)
        {
            return _context.DealershipVehicles.Where(p => p.DealershipId == dealerId).Select(c => c.Vehicle).ToList();
        }

        public bool CreateDealership(Dealership dealership)
        {
            _context.Add(dealership);

            return Save();
        }

        public bool CreateDealership_2(Dealership dealership, int vehicleId) //dealershipvehicles connection
        {
            var dealershipVehicleValue = _context.Vehicles.Where(v => v.Id == vehicleId).FirstOrDefault();

            var dealershipVehicle = new DealershipVehicle()
            {
                Vehicle = dealershipVehicleValue,
                Dealership = dealership
            };

            _context.Add(dealershipVehicle);


            _context.Add(dealership);


            return Save();
        }

        public bool Save()
        {
            var stateSaved = _context.SaveChanges();

            return stateSaved > 0 ? true : false;
        }
    }
}


//vehicle rating at 8:55

