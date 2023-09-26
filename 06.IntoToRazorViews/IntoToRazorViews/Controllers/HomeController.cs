using Microsoft.AspNetCore.Mvc;
using IntoToRazorViews.Models;

namespace IntoToRazorViews.Controllers
{
    
    public class HomeController : Controller
    {
        List<CityWeather> cities = new List<CityWeather>()
        {
            new CityWeather() {CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"),  TemperatureFahrenheit = 33},
            new CityWeather() {CityUniqueCode = "NYC", CityName = "New York City", DateAndTime = DateTime.Parse("2030-01-01 3:00"),  TemperatureFahrenheit = 60},
            new CityWeather() {CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"),  TemperatureFahrenheit = 82}
        };
        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            HttpContext.Response.StatusCode = 200;
            return View(cities);
        }

        [Route("/weather/{cityCode}")]
        public IActionResult CityWeather(string cityCode)
        {
            CityWeather? city = cities.Where(temp => temp.CityUniqueCode == cityCode).FirstOrDefault();
            if(city == null) 
            {
                HttpContext.Response.StatusCode = 400;
                return Content("City cannot be null");
            }
            HttpContext.Response.StatusCode = 200;
            return View(city);
        }
    }
}
