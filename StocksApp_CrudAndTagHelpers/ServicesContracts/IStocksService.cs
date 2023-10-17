using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts
{
    public interface IStocksService
    {
        BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest);
        SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest);
        List<BuyOrderResponse> GetBuyOrders();
        List<SellOrderResponse> GetSellOrders();
    }
}
