using DealershipApp.Data;
using DealershipApp.Interfaces;
using DealershipApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DealershipApp.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly DataContext _context;

        public StaffRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Staff> GetAllStaff()
        {
            return _context.Staff.OrderBy(p => p.Id).ToList();       
        }

        public ICollection<Dealership> GetDealerships()
        {
            return _context.Dealerships.OrderBy(c => c.Id).ToList();
        }

        public Staff GetStaffById(int id)
        {
            return _context.Staff.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Staff> GetStaffByProfession(string profession)
        {
            return _context.Staff.Where(p => p.Profession.ToLower() == profession.ToLower()).ToList();
        }

        public string GetStaffNameById(int id)
        {
            var firstName = _context.Staff.Where(p => p.Id == id).Select(c => c.FirstName).FirstOrDefault();
            var lastName = _context.Staff.Where(p => p.Id == id).Select(c => c.LastName).FirstOrDefault();
            return firstName + " " + lastName;
        }

        public bool CreateStaff(Staff staff)
        {
            _context.Add(staff);

            return Save();
        }

        public bool Save()
        {
            var stateSaved = _context.SaveChanges();  

            return stateSaved > 0 ? true : false;
        }

        public bool StaffExists(int staffId)
        {
            return _context.Staff.Where(p => p.Id == staffId).Any();
        }
    }
}


//return _context.Staff.Where(p => p.Id == id).FirstOrDefault(); //Id is the column in sql