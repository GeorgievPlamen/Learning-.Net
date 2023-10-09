using System;
using Xunit;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Abstractions;

namespace TestsX
{
    public class PersonsServiceTest
    {
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _outputHelper;

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService();
            _countriesService = new CountriesService();
            _outputHelper = testOutputHelper;
        }

        #region AddPerson
        //When we supply null value as PersonAddRequest, It should throw
        //ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? request = null;
         
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _personService.AddPerson(request);
            });
        }

        //When we supply PersonName as null, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? request = new PersonAddRequest()
            {
                PersonName = null
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personService.AddPerson(request);
            });
        }

        //Proper person details, it should insert the Person into the persons list
        // and it sohuld return an object PersonResponse, which includes the newly
        //generated person Id
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            var request = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "doe.John@email.com",
                Address = "Middle of Nowhere",
                CountryId = Guid.NewGuid(),
                Gender = ServiceContracts.Enums.GenderOptions.Female,
                DateOfBirth = DateTime.Parse("1999-10-10"),
                ReceiveNewsLetters = true
            };
            //Act
            PersonResponse personResponseFromAdd = _personService.AddPerson(request);
            List<PersonResponse> persons = _personService.GetAllPersons(); 
            //Assert
            Assert.True(personResponseFromAdd.PersonId != Guid.Empty);

            Assert.Contains(personResponseFromAdd, persons);
        }
        #endregion
        #region GetPersonPersonId
        //If we supply null as PersonId, it should return null as PersonRespons
        [Fact]
        public void GetPersonByPersonId_NullPersonId()
        {
            //Arrange
            Guid? personId = null;

            //Act
            PersonResponse? personResponseFromGet = _personService.GetPersonByPersonId(personId);
            //Assert
            Assert.Null(personResponseFromGet);
        }

        //If we supply valid person Id, it should return the proper person details as response object
        [Fact]
        public void GetPersonByPersonId_ValidPersonId()
        {
            //Arrange
            CountryAddRequest country_request = new CountryAddRequest()
            { CountryName = "Canada" };
            CountryResponse countryResponse = _countriesService.AddCountry(country_request);

            //Act
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "doe@email.com",
                Address = "Middle of nowhere",
                CountryId = countryResponse.CountryID,
                DateOfBirth = DateTime.Parse("1999-01-01"),
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            PersonResponse personResponse = _personService.AddPerson(personAddRequest);

            PersonResponse personResponseFromGet = _personService.GetPersonByPersonId(personResponse.PersonId);
            //Assert
            Assert.Equal(personResponse, personResponseFromGet);

        }
        #endregion
        #region GetAllPersons
        //Should return empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            List<PersonResponse> personsFromGet = _personService.GetAllPersons();

            Assert.Empty(personsFromGet);
        }

        //If we have added persons with Add, we should get the same in the GetAllPersons
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryAddRequest countryRequest1 = new CountryAddRequest()
            { CountryName = "USA" };
            CountryAddRequest countryRequest2 = new CountryAddRequest()
            { CountryName = "Bulgaria" };

            CountryResponse countryResponse1 = _countriesService.AddCountry(countryRequest1);
            CountryResponse countryResponse2 = _countriesService.AddCountry(countryRequest2);

            PersonAddRequest personRequest1 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "John@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "john lives here",
                CountryId = countryResponse1.CountryID,
                DateOfBirth = DateTime.Parse("1999-02-02"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest personRequest2 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "smith lives here",
                CountryId = countryResponse1.CountryID,
                DateOfBirth = DateTime.Parse("2004-05-02"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest personRequest3 = new PersonAddRequest()
            {
                PersonName = "Jane",
                Email = "jane@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Female,
                Address = "jane lives here",
                CountryId = countryResponse2.CountryID,
                DateOfBirth = DateTime.Parse("1996-12-10"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> personRequests = new List<PersonAddRequest>()
            { personRequest1, personRequest2, personRequest3 };

            List<PersonResponse> personResponses = new List<PersonResponse>();

            foreach (PersonAddRequest personRequest in personRequests)
            {
                PersonResponse personResponse = _personService.AddPerson(personRequest);
                personResponses.Add(personResponse);
            }

            //print add list
            _outputHelper.WriteLine("Expected: ");
            foreach (PersonResponse element in personResponses)
            {
                _outputHelper.WriteLine(element.ToString());
            }

            //Act
            List<PersonResponse> personResponsesFromGet = _personService.GetAllPersons();

            //print actual list
            _outputHelper.WriteLine("Actual: ");
            foreach (PersonResponse element in personResponsesFromGet)
            {
                _outputHelper.WriteLine(element.ToString());
            }
            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponses)
            {
                Assert.Contains(personResponseFromAdd, personResponsesFromGet);
            }
        }
        #endregion
        #region GetFileteredPersons
        //If search text is empty and field is PersonName, returns all persons.
        [Fact]
        public void GetFileteredPersons_EmptySearchText()
        {
            //Arrange
            CountryAddRequest countryRequest1 = new CountryAddRequest()
            { CountryName = "USA" };
            CountryAddRequest countryRequest2 = new CountryAddRequest()
            { CountryName = "Bulgaria" };

            CountryResponse countryResponse1 = _countriesService.AddCountry(countryRequest1);
            CountryResponse countryResponse2 = _countriesService.AddCountry(countryRequest2);

            PersonAddRequest personRequest1 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "John@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "john lives here",
                CountryId = countryResponse1.CountryID,
                DateOfBirth = DateTime.Parse("1999-02-02"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest personRequest2 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "smith lives here",
                CountryId = countryResponse1.CountryID,
                DateOfBirth = DateTime.Parse("2004-05-02"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest personRequest3 = new PersonAddRequest()
            {
                PersonName = "Jane",
                Email = "jane@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Female,
                Address = "jane lives here",
                CountryId = countryResponse2.CountryID,
                DateOfBirth = DateTime.Parse("1996-12-10"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> personRequests = new List<PersonAddRequest>()
            { personRequest1, personRequest2, personRequest3 };

            List<PersonResponse> personResponses = new List<PersonResponse>();

            foreach (PersonAddRequest personRequest in personRequests)
            {
                PersonResponse personResponse = _personService.AddPerson(personRequest);
                personResponses.Add(personResponse);
            }

            //print add list
            _outputHelper.WriteLine("Expected: ");
            foreach (PersonResponse element in personResponses)
            {
                _outputHelper.WriteLine(element.ToString());
            }

            //Act
            List<PersonResponse> personResponsesFromGet = _personService.GetFilteredPersons(nameof(Person.PersonName),"");

            //print actual list
            _outputHelper.WriteLine("Actual: ");
            foreach (PersonResponse element in personResponsesFromGet)
            {
                _outputHelper.WriteLine(element.ToString());
            }
            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponses)
            {
                Assert.Contains(personResponseFromAdd, personResponsesFromGet);
            }
        }

        //First we add few persons, search based on same with a search string.
        //Should return matchin persons.
        [Fact]
        public void GetFileteredPersons_SeachByPersonName()
        {
            //Arrange
            CountryAddRequest countryRequest1 = new CountryAddRequest()
            { CountryName = "USA" };
            CountryAddRequest countryRequest2 = new CountryAddRequest()
            { CountryName = "Bulgaria" };

            CountryResponse countryResponse1 = _countriesService.AddCountry(countryRequest1);
            CountryResponse countryResponse2 = _countriesService.AddCountry(countryRequest2);

            PersonAddRequest personRequest1 = new PersonAddRequest()
            {
                PersonName = "Bismuth",
                Email = "John@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "john lives here",
                CountryId = countryResponse1.CountryID,
                DateOfBirth = DateTime.Parse("1999-02-02"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest personRequest2 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "smith lives here",
                CountryId = countryResponse1.CountryID,
                DateOfBirth = DateTime.Parse("2004-05-02"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest personRequest3 = new PersonAddRequest()
            {
                PersonName = "Jane",
                Email = "jane@email.com",
                Gender = ServiceContracts.Enums.GenderOptions.Female,
                Address = "jane lives here",
                CountryId = countryResponse2.CountryID,
                DateOfBirth = DateTime.Parse("1996-12-10"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> personRequests = new List<PersonAddRequest>()
            { personRequest1, personRequest2, personRequest3 };

            List<PersonResponse> personResponses = new List<PersonResponse>();

            foreach (PersonAddRequest personRequest in personRequests)
            {
                PersonResponse personResponse = _personService.AddPerson(personRequest);
                personResponses.Add(personResponse);
            }

            //print add list
            _outputHelper.WriteLine("Expected: ");
            foreach (PersonResponse element in personResponses)
            {
                _outputHelper.WriteLine(element.ToString());
            }

            //Act
            List<PersonResponse> personResponsesFromGet = _personService.GetFilteredPersons(nameof(Person.PersonName), "Sm");

            //print actual list
            _outputHelper.WriteLine("Actual: ");
            foreach (PersonResponse element in personResponsesFromGet)
            {
                _outputHelper.WriteLine(element.ToString());
            }
            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponses)
            {
                if(personResponseFromAdd.PersonName != null)
                {
                    if (personResponseFromAdd.PersonName.Contains("sm", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personResponseFromAdd, personResponsesFromGet);
                    }
                }
            }
        }
        #endregion
    }
}
