using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts
{
    public interface IFinnhubService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
