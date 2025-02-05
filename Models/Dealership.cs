namespace DealershipApp.Models
{
    public class Dealership
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsShowRoom { get; set; }
        public Country Country { get; set; }
        public ICollection<Staff> Staff { get; set; }
        //public ICollection<Service> Services { get; set; }
        public ICollection<DealershipVehicle> DealershipVehicles { get; set; }

        //test for vehicles
        //public ICollection<Vehicle> Vehicles { get; set; }
    }
}
