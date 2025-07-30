using DealershipApp.Models;

namespace DealershipApp.Interfaces
{
    public interface IVehicleRepository
    {
        ICollection<Vehicle> GetVehicles();

        Vehicle GetVehicle(int id);
        int GetVehicleYear(int year);
        string GetVehicleColourById(int id);
        bool GetVehicleIsAutomatic(int id);
        //date
        DateTime GetVehicleOrderDate(int id);
        bool VehicleExists(int vehicleId);
        decimal GetVehicleAvgYear();

        ICollection<Vehicle> GetAllVehiclesByColour(string colour);

        //create
        bool CreateVehicle(Vehicle vehicle);

        bool UpdateVehicle(Vehicle vehicle);

        bool Save();
        bool DeleteVehicle(Vehicle vehicle);
    }
}
