namespace DealershipApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VehicleYear { get; set; }
        public string Colour { get; set; }
        public bool IsAutomatic { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        //test for dealership
        //public Dealership Dealership { get; set; }
        public int StoreId { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<DealershipVehicle> DealershipVehicles { get; set; }
    }
}
