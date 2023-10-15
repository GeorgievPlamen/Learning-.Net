using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();

            if(initialize)
            {
                _countries.AddRange(new List<Country>()
                {
                    new Country()
                {
                    CountryId = Guid.Parse("79803FF6-D5B5-4EFA-B62C-C8E7A6E21EE1"),
                    CountryName = "USA"
                },
                new Country()
                {
                    CountryId = Guid.Parse("C236D156-0B30-4148-AAF2-70E5DE6FF787"),
                    CountryName = "Canada"
                },
                new Country()
                {
                    CountryId = Guid.Parse("B3E412DE-7D59-49D4-ADD6-E3CDD507FD81"),
                    CountryName = "UK"
                },
                new Country()
                {
                    CountryId = Guid.Parse("1E52E5E4-9E22-4223-B018-D7C133F857CA"),
                    CountryName = "India"
                },
                new Country()
                {
                    CountryId = Guid.Parse("01C863A3-1381-4902-85C6-6F62A0C1AA6B"),
                    CountryName = "Australia"
                }
                });
            }
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException();
            }

            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException();
            }

            if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Country name already exists");
            }

            Country country = countryAddRequest.ToCountry();
            
            country.CountryId = Guid.NewGuid();

            _countries.Add(country);

            return country.ToCountryResponse();

        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryId(Guid? countryId)
        {
            if (countryId == null) { return null; }

            Country? countryResponseFromList = _countries.FirstOrDefault(temp => temp.CountryId == countryId);

            if(countryResponseFromList == null) { return null; }

            return countryResponseFromList.ToCountryResponse();
        }
    }
}