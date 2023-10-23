using ServiceContracts.DTO;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// <returns>Returns the country object after adding it</returns>
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest); 

        /// <summary>
        /// Returns All Countries from the list
        /// </summary>
        /// <returns>All countries from the list as List of Country Response</returns>
        Task<List<CountryResponse>> GetAllCountries();

        /// <summary>
        /// Returns a country object based on the given country id
        /// </summary>
        /// <param name="countryId">CountryID (guid) to search</param>
        /// <returns>Matching country as CountryResponse</returns>
        Task<CountryResponse?> GetCountryByCountryId(Guid? countryId);

        /// <summary>
        /// Uploads countries from excel file into database
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<int> UploadCountriesFromExcelFile(IFormFile formFile);
    }
}