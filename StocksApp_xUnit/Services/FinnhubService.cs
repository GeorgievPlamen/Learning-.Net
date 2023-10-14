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


        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            string? token = _configuration["FinnhubApi:Token"];

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}")
                };
                HttpResponseMessage responseMsg = await httpClient.SendAsync(request);
                Stream stream = responseMsg.Content.ReadAsStream();
                string? response = new StreamReader(stream).ReadToEnd();

                Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
               
                return result;
            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            string? token = _configuration["FinnhubApi:Token"];


            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}")
                };
                HttpResponseMessage responseMsg = await httpClient.SendAsync(request);
                Stream responseStream = responseMsg.Content.ReadAsStream();
                string response = new StreamReader(responseStream).ReadToEnd();

                Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                return result;
            }
        }
    }
}