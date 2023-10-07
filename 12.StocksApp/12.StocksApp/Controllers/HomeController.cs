using _12.StocksApp.Models;
using _12.StocksApp.ServiceContracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace _12.StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<TradingOptions> _options;
        private readonly IFinnhubService _finnhubService;

        public HomeController(IOptions<TradingOptions> options,IFinnhubService finhubService)
        {
            _options = options;
            _finnhubService = finhubService;
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_options.Value.DefaultStockSymbol);
            Dictionary<string, object>? stockPrice = await _finnhubService.GetStockPriceQuote(_options.Value.DefaultStockSymbol);

            StockTrade stock = new StockTrade()
            {
                StockSymbol = companyProfile["ticker"].ToString(),
                StockName = companyProfile["name"].ToString(),
                Price = stockPrice["c"].ToString()
            };
            
            return View(stock);
        }
    }
}
