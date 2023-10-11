using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Represents domain model class for SellOrder
    /// </summary>
    public class SellOrder
    {
        public Guid SellOrderID { get; set; }

        [Required(ErrorMessage = "Stock symbol is required")]
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock name is required")]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 100000)]
        public double Price { get; set; }
    }
}
