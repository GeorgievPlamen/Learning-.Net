using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using ServicesContracts;
using StocksApp_CrudAndTagHelpers.Options;
using System.Globalization;

namespace StocksApp_CrudAndTagHelpers.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _options;
        private readonly IStocksService _stocksService;
        private readonly Orders _orders;

        public HomeController(IStocksService stocksService, IConfiguration configuration, IFinnhubService finnhubService, IOptions<TradingOptions> options)
        {
            _configuration = configuration;
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _options = options.Value;
            if(_orders == null)
            {
                _orders = new Orders()
                {
                    BuyOrders = _stocksService.GetBuyOrders(),
                    SellOrders = _stocksService.GetSellOrders()
                };
            }
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            string? symbol = _options.DefaultStockSymbol;
            if (symbol == null)
            {
                symbol = "MSFT";
            }
            Dictionary<string, object>? profile = await _finnhubService.GetCompanyProfile(symbol);
            Dictionary<string, object>? StockPriceQuote = await _finnhubService.GetStockPriceQuote(symbol);
            StockTrade stock = new StockTrade();
            if (profile != null && StockPriceQuote != null)
            {
                stock.StockName = profile["name"].ToString();
                stock.StockSymbol = profile["ticker"].ToString();
                stock.Quantity = 100;
                stock.Price = Convert.ToDouble(StockPriceQuote["c"].ToString(), CultureInfo.InvariantCulture);
            }
            
            return View(stock);
        }

        [HttpGet]
        [Route("Orders")]
        public IActionResult Orders()
        {
            _orders.BuyOrders?.AddRange(_stocksService.GetBuyOrders());
            _orders.SellOrders?.AddRange(_stocksService.GetSellOrders());
            return View(_orders);
        }

        [HttpPost]
        [Route("BuyOrder")]
        public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            BuyOrderResponse response = _stocksService.CreateBuyOrder(buyOrderRequest);
            if (response == null)
            {
                return RedirectToAction("Index");
            }
            _orders.BuyOrders.AddRange(_stocksService.GetBuyOrders());
            return RedirectToAction("Orders");
        }

        [HttpPost]
        [Route("SellOrder")]
        public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            SellOrderResponse response = _stocksService.CreateSellOrder(sellOrderRequest);
            if (response == null)
            {
                return RedirectToAction("Index");
            }
            _orders.SellOrders = _stocksService.GetSellOrders();
            return RedirectToAction("Orders");
        }
    }
}
