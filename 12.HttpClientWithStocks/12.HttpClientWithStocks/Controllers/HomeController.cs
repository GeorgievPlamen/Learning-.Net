using _12.HttpClientWithStocks.Models;
using _12.HttpClientWithStocks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.Extensions.Options;

namespace _12.HttpClientWithStocks.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnhubService _finnhubSerivce;
        private readonly IOptions<TradingOptions> _tradingOptions;

        public HomeController(FinnhubService finnhubService,IOptions<TradingOptions> tradingOptions )
        {
            _finnhubSerivce = finnhubService;
            _tradingOptions = tradingOptions;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if(_tradingOptions.Value.DefaulStockSymbol == null)
            {
                _tradingOptions.Value.DefaulStockSymbol = "MSFT";
            }
            Dictionary<string, object> responseDictionary =  await _finnhubSerivce.GetStockPriceQuote(_tradingOptions.Value.DefaulStockSymbol);

            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.Value.DefaulStockSymbol,
                CurrentPrice = responseDictionary["c"].ToString(),
                HighestPrice = responseDictionary["h"].ToString(),
                LowestPrice = responseDictionary["l"].ToString(),
                OpenPrice = responseDictionary["o"].ToString()

            };

            return View(stock);
        }
    }
}
