using DealershipApp.Data;
using DealershipApp.Interfaces;
using DealershipApp.Models;

namespace DealershipApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Country> GetAllCountries()
        {
            return _context.Countries.OrderBy(p => p.Id).ToList();
        }
        public Country GetCountry(int countryId)
        {
            return _context.Countries.Where(c => c.Id == countryId).FirstOrDefault();
        }

        public string GetCountryById(int CountryId)
        {
            return _context.Countries.Where(p => p.Id == CountryId).Select(c => c.Name).FirstOrDefault();   
        }

        public Country GetCountryByName(string name)
        {
            return _context.Countries.Where(p => p.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Where(p => p.Id == id).Any();
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);

            return Save();
        }

        public bool Save()
        {
            var stateSaved = _context.SaveChanges();  

            return stateSaved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);

            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }
    }
}
