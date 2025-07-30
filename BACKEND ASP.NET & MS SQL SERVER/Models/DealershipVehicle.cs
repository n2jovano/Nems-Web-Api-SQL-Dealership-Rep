namespace DealershipApp.Models
{
    public class DealershipVehicle
    {
        public int VehicleId { get; set; }
        public int DealershipId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Dealership Dealership { get; set; }
    }
}
