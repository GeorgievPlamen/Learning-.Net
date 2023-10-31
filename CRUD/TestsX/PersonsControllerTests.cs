using AutoFixture;
using Moq;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD.Controllers;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Microsoft.AspNetCore.Mvc;

namespace TestsX
{
    public class PersonsControllerTests
    {
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;
        private readonly Mock<IPersonService> _mockPersonService;
        private readonly Mock<ICountriesService> _mockCountriesService;
        private readonly Fixture _fixture;

        public PersonsControllerTests()
        {
            _fixture = new Fixture();
            _mockCountriesService = new Mock<ICountriesService>();
            _mockPersonService = new Mock<IPersonService>();
            _countriesService = _mockCountriesService.Object;
            _personService = _mockPersonService.Object;
        }

        #region Index
        [Fact]
        public async Task Index_ShouldReturnIndexViewWithPersonsList()
        {
            //Arrange
            List<PersonResponse> persons_responses = _fixture.Create<List<PersonResponse>>();

            PersonsController personsController = new PersonsController(_personService,_countriesService);
            
            _mockPersonService.Setup(temp =>
            temp.GetFilteredPersons(It.IsAny<string>(),It.IsAny<string>()))
                .ReturnsAsync(persons_responses);

            _mockPersonService.Setup(temp =>
            temp.GetSortedPersons(It.IsAny<List<PersonResponse>>(), It.IsAny<string>(),
            It.IsAny<SortOrderOptions>()))
                .ReturnsAsync(persons_responses);
            //Act
            IActionResult result = await personsController.Index(
                _fixture.Create<string>(), 
                _fixture.Create<string>(), 
                _fixture.Create<string>(),
                _fixture.Create<SortOrderOptions>());
            //Assert
            Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region Create
        [Fact]
        public async void Create_IfNoModelErrors_ToReturnCreateView()
        {
            //Arrange
            PersonAddRequest person_add_request = _fixture.Create<PersonAddRequest>();

            PersonResponse person_repsonse = _fixture.Create<PersonResponse>();

            List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();


            _mockCountriesService.Setup(temp => 
            temp.GetAllCountries())
                .ReturnsAsync(countries);
            _mockPersonService.Setup(temp =>
            temp.AddPerson(It.IsAny<PersonAddRequest>()))
                .ReturnsAsync(person_repsonse);

            PersonsController personsController = new PersonsController(_personService, _countriesService);
            //Act
            personsController.ModelState.AddModelError("PerosnName","Person Name can't be blank");

            IActionResult result = await personsController.Create(person_add_request);
            //Assert
            Assert.IsType<ViewResult>(result);
        }
        #endregion
    }
}
