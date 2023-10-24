using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using ServiceContracts;
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
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["ApiKeys:Token"]}")
            });

            string responseBody = await response.Content.ReadAsStringAsync();

            Dictionary<string, object>? responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
            
            if (responseDict == null ) 
            {
                throw new InvalidOperationException("No Response from server");
            }
            return responseDict;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMsg = await client.SendAsync(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["ApiKeys:Token"]}")
            });

            string responseBody = await responseMsg.Content.ReadAsStringAsync();
            Dictionary<string, object>? responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDict == null )
            {
                throw new InvalidOperationException("No Response from server");
            }

            return responseDict;
        }
    }
}