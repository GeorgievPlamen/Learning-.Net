using Microsoft.Extensions.Configuration;
using ServicesContracts;
using System.Net.Http;
using System.Text.Json;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private static HttpClient? _httpClient;
        private readonly string token;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpClient = _httpClientFactory.CreateClient();
            token = "cc676uaad3i9rj8tb1s0";
        }
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            if(_httpClient == null)
            {
                return null;
            }
            //HttpClient client = _httpClientFactory.CreateClient();
            HttpRequestMessage requestMsg = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}")
            };

            HttpResponseMessage responseMsg = await _httpClient.SendAsync(requestMsg);
            Stream stream = responseMsg.Content.ReadAsStream();
            string? response = new StreamReader(stream).ReadToEnd();

            Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
            return result;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            if (_httpClient == null)
            {
                return null;
            }
            HttpRequestMessage requestMsg = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}")
            };

            HttpResponseMessage responseMsg = await _httpClient.SendAsync(requestMsg);
            Stream stream = responseMsg.Content.ReadAsStream();
            string? response = new StreamReader(stream).ReadToEnd();

            Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
            return result;
        }
    }
}