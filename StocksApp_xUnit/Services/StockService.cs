using Entities;
using ServicesContracts.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StockService : IStocksService
    {
        private readonly List<BuyOrder> buyOrders;
        private readonly List<SellOrder> sellOrders;

        public StockService()
        {
            buyOrders = new List<BuyOrder>();
            sellOrders = new List<SellOrder>();
        }
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }

            Helpers.Helpers.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            buyOrders.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }

            Helpers.Helpers.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();
            sellOrders.Add(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            return buyOrders
                .OrderBy(t => t.DateAndTimeOfOrder)
                .Select(t => t.ToBuyOrderResponse())
                .ToList();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            return sellOrders
                .OrderBy(t => t.DateAndTimeOfOrder)
                .Select(t => t.ToSellOrderResponse()).ToList();
        }
    }
}
