using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HR.LeaveManagement.MVC.Controllers
{
    [Route("")]
    [Route("Home")]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

      
    }
}