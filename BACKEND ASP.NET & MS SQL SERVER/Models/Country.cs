//using System.ComponentModel.DataAnnotations; //added after to specify

using System.ComponentModel.DataAnnotations.Schema;

namespace DealershipApp.Models
{
    public class Country
    {
        //[Key] type the ctrl + .
        public int Id { get; set; }
        //[Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public ICollection<Dealership> Dealerships { get; set; }
    }
}
