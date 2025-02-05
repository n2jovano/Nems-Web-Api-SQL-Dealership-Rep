namespace DealershipApp.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Dealership> Dealerships { get; set; }
    }
}
