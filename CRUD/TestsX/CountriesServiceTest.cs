using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCoreMock;
using Moq;

namespace TestsX
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            var countriesInitialData = new List<Country>() { };
            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
                new DbContextOptionsBuilder<ApplicationDbContext>().Options);


            var dbContext = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp => temp.Countries,countriesInitialData);
            _countriesService = new CountriesService(null);
            
        }

        #region AddCountry

        //When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _countriesService.AddCountry(request);
            });
        }
        //When the CountryName is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = null
            };
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _countriesService.AddCountry(request);
            });
        }
        //When the CountryName is duplicate, it should throw ArgumentException
       /* [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arange
            CountryAddRequest? request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest? request2 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }*/
        //When you supply proper country name, it should Insert the same into a
        //list of countries.
        [Fact]
        public async Task AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "Japan"
            };
            //Act
            CountryResponse response = await _countriesService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = await _countriesService.GetAllCountries();
            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
        }

        #endregion

        #region GetAllCountries
        [Fact]
        public async void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> actual_country_response_list = await _countriesService.GetAllCountries();
            //Assert
            Assert.Empty(actual_country_response_list);
        }

        [Fact]
        public async void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> country_reqest_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() {CountryName = "USA"},
                new CountryAddRequest() {CountryName = "UK"},
            };

            //Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest countryAddRequest in country_reqest_list)
            {
                countries_list_from_add_country.Add(await _countriesService.AddCountry(countryAddRequest));
            }

            List<CountryResponse> actualCountryResponseList = await _countriesService.GetAllCountries();
            
            foreach(CountryResponse expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponseList);
            }
        }

        #endregion

        #region GetCountryByCountryId
        [Fact]
        public async Task GetCountryByCountryId_NullCountryId()
        {
            //Arrange
            Guid? countryId = null;

            //Act
            CountryResponse? country_response_from_get_method = await _countriesService.GetCountryByCountryId(countryId);
            //Assert
            Assert.Null(country_response_from_get_method);
        }

        [Fact]
        public async Task GetCountryByCountryId_ValidCountryId()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = new CountryAddRequest() 
            {
                CountryName = "China"
            };
            CountryResponse countryResponseFromAdd = await _countriesService.AddCountry(countryAddRequest);
            //Act
            CountryResponse? countryResponseFromGet = await _countriesService.GetCountryByCountryId(countryResponseFromAdd.CountryID);
            //Assert
            Assert.Equal(countryResponseFromAdd, countryResponseFromGet);
        }
        #endregion
    }
}
