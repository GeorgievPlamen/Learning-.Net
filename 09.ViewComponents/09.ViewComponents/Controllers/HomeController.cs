using _09.ViewComponents.Models;
using Microsoft.AspNetCore.Mvc;

namespace _09.ViewComponents.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/about")]

        public IActionResult About()
        {
            return View();
        }

        [Route("workers-list")]
        public IActionResult LoadFriendsList()
        {
            PersonGridModel personGridModel = new PersonGridModel()
            {
                GridTitle = "Persons",
                Persons = new List<Person>()
                {
                     new Person(){ PersonName ="John",JobTitle = "Manager"},
                     new Person(){ PersonName ="Plamen",JobTitle = "Asst Manager"},
                     new Person(){ PersonName ="William",JobTitle = "Clerk"}
                }
            };

            return ViewComponent("Grid",personGridModel);
        }
    }
}
