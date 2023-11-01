using System;
using Xunit;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Abstractions;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCoreMock;
using AutoFixture;
using FluentAssertions;
using RepositoryContracts;
using Moq;

namespace TestsX
{
    public class PersonsServiceTest
    {
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;
        private readonly Mock<IPersonsRepository> _personsRepositoryMock;
        private readonly IPersonsRepository _presonsRepository;
        private readonly ITestOutputHelper _outputHelper;
        private readonly IFixture _fixture;

        /*public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _personsRepositoryMock = new Mock<IPersonsRepository>();
            _presonsRepository = _personsRepositoryMock.Object;

            var countriesInitialData = new List<Country>() { };
            var personsInitialData = new List<Person>() { };

            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
                new DbContextOptionsBuilder<ApplicationDbContext>().Options);

            var dbContext = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);
            dbContextMock.CreateDbSetMock(temp => temp.Persons, personsInitialData);


            _countriesService = new CountriesService(null);
            _personService = new PersonService(_presonsRepository);

            _outputHelper = testOutputHelper;
        }*/

        #region AddPerson
        //When we supply null value as PersonAddRequest, It should throw
        //ArgumentNullException
        [Fact]
        public async Task AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? request = null;
         
            //Assert
            Func<Task> action = (async () =>
            {
                //Act
                await _personService.AddPerson(request);
            });

            await action.Should().ThrowAsync<ArgumentNullException>();

            //await Assert.ThrowsAsync<ArgumentNullException>
        }

        //When we supply PersonName as null, it should throw ArgumentNullException
        [Fact]
        public async Task AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? request = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.PersonName, null as string)
                .Create();

            Person person = request.ToPerson();

            _personsRepositoryMock.Setup(temp => temp.AddPerson(It.IsAny<Person>())).ReturnsAsync(person);

            //Assert
            Func<Task> action = async() =>
            {
                //Act
                await _personService.AddPerson(request);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //Proper person details, it should insert the Person into the persons list
        // and it sohuld return an object PersonResponse, which includes the newly
        //generated person Id
        [Fact]
         public async Task AddPerson_FullPersonDetails_ToBeSuccessful()
        {
            //Arrange
            var request = 
                _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();

            Person person = request.ToPerson();
            PersonResponse person_response_expected = person.ToPersonResponse();

            //If we supply any argument value, it should return the same value
            _personsRepositoryMock.Setup(
                temp => temp.AddPerson(It.IsAny<Person>()))
                .ReturnsAsync(person);

            //Act
            PersonResponse personResponseFromAdd = await _personService.AddPerson(request);
            //Assert
            //Assert.True(personResponseFromAdd.PersonId != Guid.Empty);
            personResponseFromAdd.PersonId.Should().NotBe(Guid.Empty);
        }
        #endregion
        #region GetPersonPersonId
        //If we supply null as PersonId, it should return null as PersonRespons
        [Fact]
        public async Task GetPersonByPersonId_NullPersonId()
        {
            //Arrange
            Guid? personId = null;

            //Act
            PersonResponse? personResponseFromGet = await _personService.GetPersonByPersonId(personId);
            //Assert
            Assert.Null(personResponseFromGet);
        }

        //If we supply valid person Id, it should return the proper person details as response object
        [Fact]
        public async Task GetPersonByPersonId_ValidPersonId()
        {
            //Arrange
            Person personRequest = _fixture.Build<Person>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();
            PersonResponse person_response_expected = personRequest.ToPersonResponse();

            _personsRepositoryMock.Setup(temp =>
            temp.GetPersonByPersonID(It.IsAny<Guid>()))
                .ReturnsAsync(personRequest);
            //Act


            PersonResponse? personResponseFromGet = await _personService.GetPersonByPersonId(personRequest.PersonId);
            //Assert
            personResponseFromGet.Should().Be(person_response_expected);
        }
        #endregion
        #region GetAllPersons
        //Should return empty list by default
        [Fact]
        public async Task GetAllPersons_EmptyList()
        {
            //Arrange
            var persons = new List<Person>();
            _personsRepositoryMock.Setup(temp =>
            temp.GetAllPersons())
                .ReturnsAsync(persons);

            List<PersonResponse> personsFromGet = await _personService.GetAllPersons();

            Assert.Empty(personsFromGet);
        }

        //If we have added persons with Add, we should get the same in the GetAllPersons
        [Fact]
        public async Task GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryAddRequest countryRequest1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest countryRequest2 = _fixture.Create<CountryAddRequest>();

            CountryResponse countryResponse1 = await _countriesService.AddCountry(countryRequest1);
            CountryResponse countryResponse2 = await _countriesService.AddCountry(countryRequest2);

            PersonAddRequest personRequest1 = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();

            PersonAddRequest personRequest2 = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();

            PersonAddRequest personRequest3 = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();

            List<PersonAddRequest> personRequests = new List<PersonAddRequest>()
            { personRequest1, personRequest2, personRequest3 };

            List<PersonResponse> personResponses = new List<PersonResponse>();

            foreach (PersonAddRequest personRequest in personRequests)
            {
                PersonResponse personResponse = await _personService.AddPerson(personRequest);
                personResponses.Add(personResponse);
            }

            //print add list
            _outputHelper.WriteLine("Expected: ");
            foreach (PersonResponse element in personResponses)
            {
                _outputHelper.WriteLine(element.ToString());
            }

            //Act
            List<PersonResponse> personResponsesFromGet = await _personService.GetAllPersons();

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
        public async Task GetFileteredPersons_EmptySearchText()
        {
            //Arrange
            CountryAddRequest countryRequest1 = new CountryAddRequest()
            { CountryName = "USA" };
            CountryAddRequest countryRequest2 = new CountryAddRequest()
            { CountryName = "Bulgaria" };

            CountryResponse countryResponse1 = await _countriesService.AddCountry(countryRequest1);
            CountryResponse countryResponse2 = await _countriesService.AddCountry(countryRequest2);

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
                PersonResponse personResponse = await _personService.AddPerson(personRequest);
                personResponses.Add(personResponse);
            }

            //print add list
            _outputHelper.WriteLine("Expected: ");
            foreach (PersonResponse element in personResponses)
            {
                _outputHelper.WriteLine(element.ToString());
            }

            //Act
            List<PersonResponse> personResponsesFromGet = await _personService.GetFilteredPersons(nameof(Person.PersonName),"");

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
        public async Task GetFileteredPersons_SeachByPersonName()
        {
            //Arrange
            CountryAddRequest countryRequest1 = new CountryAddRequest()
            { CountryName = "USA" };
            CountryAddRequest countryRequest2 = new CountryAddRequest()
            { CountryName = "Bulgaria" };

            CountryResponse countryResponse1 = await _countriesService.AddCountry(countryRequest1);
            CountryResponse countryResponse2 = await _countriesService.AddCountry(countryRequest2);

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
                PersonResponse personResponse = await _personService.AddPerson(personRequest);
                personResponses.Add(personResponse);
            }

            //print add list
            _outputHelper.WriteLine("Expected: ");
            foreach (PersonResponse element in personResponses)
            {
                _outputHelper.WriteLine(element.ToString());
            }

            //Act
            List<PersonResponse> personResponsesFromGet = await _personService.GetFilteredPersons(nameof(Person.PersonName), "Sm");

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
        #region GetSortedPersons
        //When we sort based on PersonName in DESC, should return list in descending on PersonName
        [Fact]
        public async Task GetSortedPersons()
        {
            //Arrange
            CountryAddRequest countryRequest1 = new CountryAddRequest()
            { CountryName = "USA" };
            CountryAddRequest countryRequest2 = new CountryAddRequest()
            { CountryName = "Bulgaria" };

            CountryResponse countryResponse1 = await _countriesService.AddCountry(countryRequest1);
            CountryResponse countryResponse2 = await _countriesService.AddCountry(countryRequest2);

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
                PersonResponse personResponse = await _personService.AddPerson(personRequest);
                personResponses.Add(personResponse);
            }

            //print add list
            _outputHelper.WriteLine("Expected: ");
            foreach (PersonResponse element in personResponses)
            {
                _outputHelper.WriteLine(element.ToString());
            }

            List<PersonResponse> allPersons = await _personService.GetAllPersons();

            //Act
            List<PersonResponse> personResponsesFromSort = await _personService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC);

            //print actual list
            _outputHelper.WriteLine("Actual: ");
            foreach (PersonResponse element in personResponsesFromSort)
            {
                _outputHelper.WriteLine(element.ToString());
            }

            personResponses = personResponses.OrderByDescending(temp => temp.PersonName).ToList();

            //Assert
            for (int i = 0; i< personResponses.Count; i++)
            {
                Assert.Equal(personResponses[i], personResponsesFromSort[i]);
            }
        }
        #endregion
        #region UpdatePerson
        //When we supply null as request, should throw ArgumentNullException
        [Fact]
        public async Task UpdatePerson_NullPerson()
        {   
            //Arrange
            PersonUpdateRequest? personUpdateRequest = null;
           
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _personService.UpdatePerson(personUpdateRequest);
            });
        }

        //When we supply invalid personId, should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_InvalidPersonId()
        {
            //Arrange
            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest()
            { PersonId = Guid.NewGuid()};

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                //Act
                await _personService.UpdatePerson(personUpdateRequest);
            });
        }

        //When we supply null as PersonName, should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_PersonNameIsNull()
        {
            //Arrange
            CountryAddRequest countryAddRequest = _fixture.Create<CountryAddRequest>();
            CountryResponse countryResponse = await _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();

            PersonResponse personResponseFromAdd = await _personService.AddPerson(personAddRequest);

            PersonUpdateRequest personUpdateRequest = personResponseFromAdd.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                //Act
                await _personService.UpdatePerson(personUpdateRequest);
            });
        }

        //First we add a new person and try to update the same
        [Fact]
        public async Task UpdatePerson_PersonFullDetails()
        {
            //Arrange
            CountryAddRequest countryAddRequest = _fixture.Create<CountryAddRequest>();
            CountryResponse countryResponse = await _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();

            PersonResponse personResponseFromAdd = await _personService.AddPerson(personAddRequest);

            PersonUpdateRequest personUpdateRequest = personResponseFromAdd.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = "William";
            personUpdateRequest.Email = "william@example.com";

            //Act
            PersonResponse personResponseFromUpdate = await _personService.UpdatePerson(personUpdateRequest);
            PersonResponse? personResponseFromGet = await _personService.GetPersonByPersonId(personResponseFromUpdate.PersonId);

            //Assert
            Assert.Equal(personResponseFromGet, personResponseFromUpdate);
        }
        #endregion
        #region DeletePerson
        //If you supply a valid PersonId, it should return true
        [Fact]
        public async Task DeletePerson_ValidPersonId()
        {
            //Arrange
            CountryAddRequest countryAddRequest = _fixture.Create<CountryAddRequest>();
            CountryResponse countryResponse = await _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();

            PersonResponse personResponseFromAdd = await _personService.AddPerson(personAddRequest);

            //Act
            bool isDeleted = await _personService.DeletePerson(personResponseFromAdd.PersonId);
        
            //Assert
            Assert.True(isDeleted);
        }

        //If you supply a invalid PersonId, it should return false
        [Fact]
        public async Task DeletePerson_InvalidPersonId()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            { CountryName = "UK" };
            CountryResponse countryResponse = await _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "someone@example.com")
                .Create();

            PersonResponse personResponseFromAdd = await _personService.AddPerson(personAddRequest);

            //Act
            bool isDeleted = await _personService.DeletePerson(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }
        #endregion
    }
}
