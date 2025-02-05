using DealershipApp.Models;

namespace DealershipApp.Dto
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string TypeofService { get; set; }
        public DateTime RepairDate { get; set; }
        public string Description { get; set; }
    }
}
