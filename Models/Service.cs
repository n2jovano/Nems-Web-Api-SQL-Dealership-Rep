namespace DealershipApp.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string TypeofService { get; set; }
        public DateTime RepairDate { get; set; }
        public string Description { get; set; }
        public Vehicle Vehicle { get; set; }

        public int VehicleId { get; set; }
    }
}
