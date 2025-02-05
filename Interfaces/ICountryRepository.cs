using DealershipApp.Models;

namespace DealershipApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetAllCountries();
        string GetCountryById (int CountryId);
        Country GetCountryByName (string name);
        Country GetCountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool Save();
    }
}
