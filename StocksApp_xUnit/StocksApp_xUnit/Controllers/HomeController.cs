using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp_xUnit.Models;
using System.Runtime.CompilerServices;

namespace StocksApp_xUnit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly FinnhubOptions _options;

        public HomeController(IOptions<FinnhubOptions> options, IConfiguration configuration)
        {
            _options = options.Value;
            _configuration = configuration;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Token = _options.Token;
            ViewBag.Symbol = _configuration["TradingOptions:DefaultStockSymbol"];
            return View();
        }
    }
}
