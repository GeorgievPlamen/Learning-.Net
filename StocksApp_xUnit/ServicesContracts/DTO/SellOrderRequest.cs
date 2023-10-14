using Entities;
using ServicesContracts.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts.DTO
{
    public class SellOrderRequest
    {
        [Required(ErrorMessage = "Symbol is required")]
        public string? StockSymbol { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? StockName { get; set; }
        [MinimumYearValidator]
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 10000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }

        public SellOrder ToSellOrder()
        {
            return new SellOrder()
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}
