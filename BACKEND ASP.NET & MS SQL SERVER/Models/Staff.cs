namespace DealershipApp.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Profession { get; set; }
        public Dealership Dealership { get; set; }
    }
}
