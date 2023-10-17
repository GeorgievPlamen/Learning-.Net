using Enteties;
using Services.Helpers;
using ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _BuyOrders;
        private readonly List<SellOrder> _SellOrders;

        public StocksService()
        {
            _BuyOrders = new List<BuyOrder>();
            _SellOrders = new List<SellOrder>();
        }
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }
            ModelValidation.Validate(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderId = Guid.NewGuid();
            _BuyOrders?.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if(sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }
            ModelValidation.Validate(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderId = Guid.NewGuid();
            _SellOrders?.Add(sellOrder);

            return  sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            return _BuyOrders
                .OrderBy(t => t.DateAndTimeOfOrder)
                .Select(t => t.ToBuyOrderResponse())
                .ToList();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            return _SellOrders
                .OrderBy(t => t.DateAndTimeOfOrder)
                .Select(t => t.ToSellOrderResponse())
                .ToList();
        }
    }
}
