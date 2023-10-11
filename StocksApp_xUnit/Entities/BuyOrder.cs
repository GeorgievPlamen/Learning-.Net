using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Represents the domain model class for a BuyOrder
    /// </summary>
    public class BuyOrder
    {
        public Guid BuyOrderID { get; set; }

        [Required(ErrorMessage ="Stock symbol is required")]
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock name is required")]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1,100000)]
        public uint Quantity { get; set; }
        [Range(1, 100000)]
        public double Price { get; set; }
    }
}