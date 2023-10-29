using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException();
            }

            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException();
            }

            if (await _countriesRepository.GetCountryByCountryName(countryAddRequest.CountryName) != null)
            {
                throw new ArgumentException("Country name already exists");
            }

            Country country = countryAddRequest.ToCountry();
            
            country.CountryId = Guid.NewGuid();

            await _countriesRepository.AddCountry(country);

            return country.ToCountryResponse();

        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return (await _countriesRepository.GetAllCountries()).Select(countryy => countryy.ToCountryResponse()).ToList();
        }
        public async Task<CountryResponse?> GetCountryByCountryId(Guid? countryId)
        {
            if (countryId == null) { return null; }

            Country? countryResponseFromList = await _countriesRepository.GetCountryByCountryID(countryId.Value);

            if(countryResponseFromList == null) { return null; }

            return countryResponseFromList.ToCountryResponse();
        }

        public async Task<int> UploadCountriesFromExcelFile(IFormFile formFile)
        {
            MemoryStream memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            int countriesInserted = 0;

            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets["Countries"];

                int rowCount = workSheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++)
                {
                    string? cellValue = Convert.ToString(workSheet.Cells[row, 1].Value);

                    if(!string.IsNullOrEmpty(cellValue))
                    {
                        string countryName = cellValue;

                        if (await _countriesRepository.GetCountryByCountryName(countryName) == null)
                            {
                            Country country = new Country() { CountryName = countryName };
                            await _countriesRepository.AddCountry(country);
                            countriesInserted++;
                            }
                    }
                }
            }
            return countriesInserted;
        }
    }
}