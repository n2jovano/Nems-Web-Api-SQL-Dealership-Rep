using DealershipApp.Dto;
using DealershipApp.Models;

namespace DealershipApp.Interfaces
{
    public interface IDealershipRepository
    {
        ICollection<Dealership> GetDealerships();   //(a) outputting a list

        Dealership GetDealership(int id);
        Dealership GetDealership(string name);
        Dealership GetDealership(bool isShowroom);

        bool DealershipExists(int dealerId);

        ICollection<Vehicle> GetVehiclesByDealership(int dealerId);
        bool CreateDealership(Dealership dealership);
        bool CreateDealership_2(Dealership dealership, int vehicle);
        bool Save();
    }
}
