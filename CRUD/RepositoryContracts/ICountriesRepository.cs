using Entities;

namespace RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Country entity
    /// </summary>
    public interface ICountriesRepository
    {
        /// <summary>
        /// Adds a new country object to the data store
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public Task<Country> AddCountry(Country country);
        public Task<List<Country>> GetAllCountries();
        public Task<Country?> GetCountryByCountryID(Guid countryID);
        public Task<Country?> GetCountryByCountryName(string countryName);
    }
}