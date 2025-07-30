using DealershipApp.Models;

namespace DealershipApp.Interfaces
{
    public interface IStaffRepository
    {
        ICollection<Staff> GetAllStaff();
        Staff GetStaffById(int id);
        string GetStaffNameById(int id);
        ICollection<Staff> GetStaffByProfession(string profession);
        bool StaffExists(int staffId);
        ICollection<Dealership> GetDealerships();
        bool CreateStaff(Staff staff);
        bool Save();
    }
}
