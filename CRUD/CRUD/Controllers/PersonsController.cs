using CRUD.Filters;
using CRUD.Filters.ActionFilters;
using CRUD.Filters.AuthorizationFilters;
using CRUD.Filters.ExceptionFilters;
using CRUD.Filters.ResourceFilters;
using CRUD.Filters.ResultFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUD.Controllers
{
    [Route("[controller]")]
    //[TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] { "my-class-key", "my-class-value", 3 })]
    [ResponseHeaderFilterFactoryClass("my-class-key", "my-class-value", 3)]
    [TypeFilter(typeof(HandleExceptionFilter))]
    [TypeFilter(typeof(PersonsAlwaysRunResultFilter))]
    public class PersonsController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ILogger<PersonsController> _logger;
        public PersonsController(ILogger<PersonsController> logger, IPersonService personService, ICountriesService countriesService)
        {
            _personService = personService;
            _countriesService = countriesService;
            _logger = logger;
        }

        [Route("/")]
        [Route("index")]
        [TypeFilter(typeof(PersonsListActionFilter), Order = 4)]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] {"X-Customer-Key","Custom-Value", 3})]
        public async Task<IActionResult> Index(
            string searchBy,
            string? searchString,
            string sortBy = nameof(PersonResponse.PersonName),
            SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            _logger.LogInformation("Index action method of PersonsController");
            _logger.LogDebug(
                $@"searchBy: {searchBy},
                searchString: {searchString}, 
                sortBy: {sortBy}, 
                sortOrder: {sortOrder}");
            
            //Search
            
            List<PersonResponse> persons = await _personService.GetFilteredPersons(searchBy, searchString);

            //Sort
            List<PersonResponse> sortedPersons = await _personService.GetSortedPersons(persons, sortBy, sortOrder);

            return View(sortedPersons);
        }

        [Route("create")]
        [HttpGet]
        //[TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] {"my-key","my-value", 4})]
        [ResponseHeaderFilterFactoryClass("my-class-key", "my-class-value", 3)]
        [TypeFilter(typeof(FeatureDisabledResourceFilter), Arguments = new object[] { false })]
        [SkipFilter]
        public async Task<IActionResult> Create()
        {
            List<CountryResponse> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp =>
                new SelectListItem()
                {
                    Text = temp.CountryName,
                    Value = temp.CountryID.ToString()
                });

            return View();
        }

        [Route("create")]
        [HttpPost]
        [TypeFilter(typeof(PersonCreateAndEditPostActionFilter))]
        public async Task<IActionResult> Create(PersonAddRequest personRequest)
        {
            PersonResponse personResponse = await _personService.AddPerson(personRequest);
            return RedirectToAction("Index", "Persons");
        }

        [Route("[action]/{personId}")]
        [HttpGet]
        [TypeFilter(typeof(TokenResultFilter))]
        public async Task<IActionResult> Edit(Guid personId)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonId(personId);
            if (personResponse == null)
            {
                return RedirectToAction("Index");
            }

            List<CountryResponse> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp =>
                new SelectListItem()
                {
                    Text = temp.CountryName,
                    Value = temp.CountryID.ToString()
                });

            PersonUpdateRequest personRequest = personResponse.ToPersonUpdateRequest();
            return View(personRequest);
        }

        [Route("[action]/{personId}")]
        [HttpPost]
        [TypeFilter(typeof(TokenAuthorizationFilter))]
        [TypeFilter(typeof(PersonCreateAndEditPostActionFilter))]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonId(personUpdateRequest.PersonId);
            if(personResponse == null)
            {
                return RedirectToAction("Index");
            }

                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
        }

        [HttpGet]
        [Route("[action]/{personId}")]
        public async Task<IActionResult> Delete(Guid? personId)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonId(personId);
            if (personResponse == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(personResponse);
        }

        [HttpPost]
        [Route("[action]/{personId}")]
        public async Task<IActionResult> Delete(PersonResponse personResponse)
        {
            if (ModelState.IsValid)
            {
                await _personService.DeletePerson(personResponse.PersonId);
                return RedirectToAction("Index");
            }
            else
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
        }

        [Route("PersonsPDF")]
        public async Task<IActionResult> ForPDF()
        {
            //Get List Of persons
            List<PersonResponse> persons = await _personService.GetAllPersons();

            //Return view as pdf
            return new ViewAsPdf("ForPDF", persons, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Top = 20, Right = 20, Bottom = 20, Left = 20
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }

        [Route("PersonCSV")]
        public async Task<IActionResult> PersonsCSV()
        {
            MemoryStream memory = await _personService.GetPersonsCSV();
            return File(memory, "application/octet-stream", "persons.csv");
        }

        [Route("PersonExcel")]
        public async Task<IActionResult> PersonsExcel()
        {
            MemoryStream memory = await _personService.GetPersonsExcel();
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "persons.xlsx");
        }
    }
}
