using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace _12.Configuration.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherApiOptions _options;

        public HomeController(IOptions<WeatherApiOptions> weatherApiOptions)
        {
            _options = weatherApiOptions.Value;
        }


        [Route("/")]
        public IActionResult Index()
        {
            /*ViewBag.MyKey = _configuration["MyKey"];*/
            /*ViewBag.weatherApi = _configuration["weatherApi:ClientID"];*/
            /*ViewBag.weatherApi = _configuration.GetSection("weatherApi")["ClientID"];*/
            /*WeatherApiOptions options = _configuration.GetSection("weatherApi").Get<WeatherApiOptions>();*/

            ViewBag.ClientID = _options.ClientID;
            ViewBag.ClientSecret = _options.ClientSecret;



            return View();
        }
    }
}
