namespace DealershipApp.Dto
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VehicleYear { get; set; }
        public string Colour { get; set; }
        public bool IsAutomatic { get; set; }
        public DateTime OrderDate { get; set; }
        public int StoreId { get; set; }
    }
}
