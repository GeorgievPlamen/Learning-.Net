using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        public PersonService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();

            if(initialize)
            {
                _persons.Add(new Person()
                {
                    PersonId = Guid.Parse("705201EB-C371-4207-A7BB-41B18572655F"),
                    PersonName = "Robinetta",
                    Email = "rfryatt0@omniture.com",
                    DateOfBirth = DateTime.Parse("1988-4-2"),
                    Gender = "Female",
                    Address = "13 Lien Center", 
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("79803FF6-D5B5-4EFA-B62C-C8E7A6E21EE1")
                });
                _persons.Add(new Person()
                {
                    PersonId = Guid.Parse("B8A30DBF-654B-4636-9175-73CE386D78D1"),
                    PersonName = "Theresa",
                    Email = "tshepland1@omniture.com",
                    DateOfBirth = DateTime.Parse("1988-4-4"),
                    Gender = "Female",
                    Address = "13 Lien Center",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("B3E412DE-7D59-49D4-ADD6-E3CDD507FD81")
                });
                _persons.Add(new Person()
                {
                    PersonId = Guid.Parse("D6BEC070-E2A2-4BB5-8392-7F466B847B47"),
                    PersonName = "Sutherland",
                    Email = "jgiacobbo3@surveymonkey.com",
                    DateOfBirth = DateTime.Parse("1992-8-6"),
                    Gender = "Male",
                    Address = "13 Lien Center",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("79803FF6-D5B5-4EFA-B62C-C8E7A6E21EE1")
                });
                _persons.Add(new Person()
                {
                    PersonId = Guid.Parse("0C72A483-3ED3-45FC-A8A8-EA2D940ECF01"),
                    PersonName = "Jessee",
                    Email = "siddenden2@illinois.edu",
                    DateOfBirth = DateTime.Parse("2001-5-3"),
                    Gender = "Male",
                    Address = "13 Lien Center",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("79803FF6-D5B5-4EFA-B62C-C8E7A6E21EE1")
                });
                _persons.Add(new Person()
                {
                    PersonId = Guid.Parse("F353BC8B-0967-4975-AA56-3311954241FC"),
                    PersonName = "Yancy",
                    Email = "ysealand4@cloudflare.com",
                    DateOfBirth = DateTime.Parse("1996-10-11"),
                    Gender = "Female",
                    Address = "13 Lien Center",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("01C863A3-1381-4902-85C6-6F62A0C1AA6B")
                });
                _persons.Add(new Person()
                {
                    PersonId = Guid.Parse("1A183779-207B-4D42-BBB6-2AE6D42F0BF3"),
                    PersonName = "Deni",
                    Email = "dreynoollds5@creativecommons.org",
                    DateOfBirth = DateTime.Parse("1988-4-2"),
                    Gender = "Female",
                    Address = "13 Lien Center",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("C236D156-0B30-4148-AAF2-70E5DE6FF787")
                });
            }
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();

            personResponse.Country = _countriesService.GetCountryByCountryId(personResponse.CountryId)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Model Validations
            ValidationHelper.ModelValidation(personAddRequest);

            var person = personAddRequest.ToPerson();

            person.PersonId = Guid.NewGuid();

            _persons.Add(person);

            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(temp => temp.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonId(Guid? personId)
        {
            if (personId == null) return null;

            Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
            if (person == null) return null;

            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            {
                return matchingPersons;
            }

            switch (searchBy)
            {
                case nameof(Person.PersonId):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.PersonId.ToString()) ?
                    temp.PersonId.ToString().Contains(searchString ,StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.PersonName) ?
                    temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(temp =>
                    (temp.DateOfBirth != null) ?
                    temp.DateOfBirth.Value
                    .ToString("dd MMMM yyyy")
                    .Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)
                    .ToList();
                    break;
                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.Equals(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.CountryId):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.Address):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default: matchingPersons = allPersons; break;
            }

            return matchingPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string SortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(SortBy)) return allPersons;

            List<PersonResponse> sortedPersons = (SortBy, sortOrder)
            switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC)
                => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC)
                => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC)
                => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),


                (nameof(PersonResponse.Address), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),


                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.PersonName).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.PersonName).ToList(),

                _ => allPersons
            };

            return sortedPersons;
        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if(personUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(Person));
            }

            ValidationHelper.ModelValidation(personUpdateRequest);

            Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonId == personUpdateRequest.PersonId);
            if (matchingPerson == null)
            {
                throw new ArgumentException("Person Id does not exist");
            }

            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryId = personUpdateRequest.CountryId;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personId)
        {
            if (personId == null)
            {
                throw new ArgumentNullException(nameof(personId));
            }

            Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
            if (person == null) { return false; }

            _persons.Remove(person);

            return true;
        }
    }
}
