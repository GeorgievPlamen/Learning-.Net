using _12.StocksApp.ServiceContracts;
using System.Text.Json;

namespace _12.StocksApp.Services
{
    public class FinnhubCompanyProfileService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public FinnhubCompanyProfileService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using (HttpClient FinnhubClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage() 
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token=cc676uaad3i9rj8tb1s0")
                };
                HttpResponseMessage httpResponse = await FinnhubClient.SendAsync(httpRequestMessage);
                Stream stream = httpResponse.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);

                string response = reader.ReadToEnd();
                Dictionary<string, object>? responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if(responseDict == null)
                {
                    throw new InvalidOperationException("No response from finnhub server");
                }

                if(responseDict.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDict["error"]));
                }

                return responseDict;
            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token=cc676uaad3i9rj8tb1s0")
                };
                HttpResponseMessage responseMsg = await httpClient.SendAsync(httpRequestMessage);
                Stream stream = responseMsg.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);

                string response = reader.ReadToEnd();
                Dictionary<string, object> responseDict = JsonSerializer.Deserialize<Dictionary<string,object>>(response);
                if(responseDict == null) { throw new InvalidOperationException(); };
                if (responseDict.ContainsKey("error")) { throw new InvalidOperationException(); };

                return responseDict;
            }
        }
    }
}
