using Microsoft.AspNetCore.Mvc;
using ServicesContracts;

namespace StocksApp_CrudAndTagHelpers.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _finnhubService;

        public HomeController(IConfiguration configuration, IFinnhubService finnhubService)
        {
            _configuration = configuration;
            _finnhubService = finnhubService;
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            string? symbol = _configuration["TradingOptions:DefaultStockSymbol"];
            if (symbol == null)
            {
                symbol = "MSFT";
            }
            Dictionary<string, object>? profile = await _finnhubService.GetCompanyProfile(symbol);
            Dictionary<string, object>? StockPriceQuote = await _finnhubService.GetStockPriceQuote(symbol);
            return View();
        }
    }
}
