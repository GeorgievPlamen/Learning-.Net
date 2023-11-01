using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUD.Controllers
{
    [Route("[controller]")]
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
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(PersonResponse.PersonName), "Person Name" },
                { nameof(PersonResponse.Email), "Email" },
                { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
                { nameof(PersonResponse.Gender), "Gender" },
                { nameof(PersonResponse.Country), "Country" },
                { nameof(PersonResponse.Address), "Address" } 
            };
            List<PersonResponse> persons = await _personService.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sort
            List<PersonResponse> sortedPersons = await _personService.GetSortedPersons(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons);
        }

        [Route("create")]
        [HttpGet]
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
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            PersonResponse personResponse = await _personService.AddPerson(personAddRequest);
            return RedirectToAction("Index", "Persons");
        }

        [Route("[action]/{personId}")]
        [HttpGet]
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

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            return View(personUpdateRequest);
        }

        [Route("[action]/{personId}")]
        [HttpPost]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonId(personUpdateRequest.PersonId);
            if(personResponse == null)
            {
                return RedirectToAction("Index");
            }

            if(ModelState.IsValid)
            {
                PersonResponse updatedPerson = await _personService.UpdatePerson(personUpdateRequest);
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
