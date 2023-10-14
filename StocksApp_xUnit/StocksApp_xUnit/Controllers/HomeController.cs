using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using ServicesContracts;
using ServicesContracts.DTO;
using StocksApp_xUnit.Models;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace StocksApp_xUnit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly FinnhubOptions _options;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;

        public HomeController(IFinnhubService finnhubService, IOptions<FinnhubOptions> options, IConfiguration configuration, IStocksService stocksService)
        {
            _options = options.Value;
            _configuration = configuration;
            _finnhubService = finnhubService;
            _stocksService = stocksService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            string? symbol = _configuration["TradingOptions:DefaultStockSymbol"];
            if (symbol == null) { symbol = "MSFT"; }

            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(symbol);
            Dictionary<string, object>? stockPrice = await _finnhubService.GetStockPriceQuote(symbol);
            StockTrade stock = new StockTrade() { StockSymbol = symbol};
            if (companyProfile != null && stockPrice != null)
            {
                stock = new StockTrade()
                {
                    StockSymbol = symbol,
                    StockName = companyProfile["name"].ToString(),
                    Price = Convert.ToDouble(stockPrice["c"].ToString(),CultureInfo.InvariantCulture)
                };
            }

            BuyOrderResponse response = _stocksService.CreateBuyOrder(new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-12-31"),
                Quantity = 10,
                Price = 100
            });

            ViewBag.Token = _options.Token;

            return View(stock);
        }
    }
}
