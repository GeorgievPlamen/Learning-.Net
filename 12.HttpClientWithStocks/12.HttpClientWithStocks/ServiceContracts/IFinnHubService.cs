namespace _12.HttpClientWithStocks.ServiceContracts
{
    public interface IFinnHubService
    {
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
