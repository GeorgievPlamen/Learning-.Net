using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using StocksApp_EntityFramework.Models;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace StocksApp_EntityFramework.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly TradingOptions _options;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;

        public TradeController(IOptions<TradingOptions> options,
            IConfiguration configuration,
            IFinnhubService finnhubService,
            IStocksService stocksService)
        {
            _configuration = configuration;
            _options = options.Value;
            _finnhubService = finnhubService;
            _stocksService = stocksService;
        }

        [Route ("/")]
        [Route ("Trade")]
        public async Task<IActionResult> Trade()
        {
            if(string.IsNullOrEmpty(_options.DefaultStockSymbol))
            {
                _options.DefaultStockSymbol = "MSFT";
            }

            StockTrade stock = new StockTrade();

            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_options.DefaultStockSymbol);
            Dictionary<string, object>? stockPriceQuote = await _finnhubService.GetStockPriceQuote(_options.DefaultStockSymbol);

            if (companyProfile == null || stockPriceQuote == null)
            {
                throw new ArgumentNullException();
            }


            stock.StockSymbol = "MSFT";
            stock.StockName = companyProfile["name"].ToString();
            stock.Price = Convert.ToDouble(stockPriceQuote["c"].ToString(), CultureInfo.InvariantCulture);
            stock.Quantity = Convert.ToUInt32(_options.DefaultOrderQuantity,CultureInfo.InvariantCulture);

            return View(stock);
        }

        [Route ("BuyOrder")]
        [HttpPost]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest request)
        {
            request.DateAndTimeOfOrder = DateTime.Now;
            BuyOrderResponse response = await _stocksService.CreateBuyOrder(request);
            return RedirectToAction("Trade");
        }

        [Route("SellOrder")]
        [HttpPost]
        public async Task<IActionResult> SellOrder(SellOrderRequest request)
        {
            request.DateAndTimeOfOrder = DateTime.Now;
            SellOrderResponse response = await _stocksService.CreateSellOrder(request);
            return RedirectToAction("Trade");
        }

        [Route("Orders")]
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
           List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
           List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();
           
           Orders orders = new Orders()
           {
               BuyOrders = buyOrders,
               SellOrders = sellOrders
           };

           return View(orders);
        }

        [Route("OrdersPDF")]
        [HttpGet]
        public async Task<IActionResult> OrdersPDF()
        {
            return View();
        }
    }
}
