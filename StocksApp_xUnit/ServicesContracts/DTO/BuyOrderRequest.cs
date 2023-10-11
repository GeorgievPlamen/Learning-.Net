using ServicesContracts.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts.DTO
{
    public class BuyOrderRequest
    {
        [Required(ErrorMessage ="Symbol is required")]
        public string? StockSymbol { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? StockName { get; set; }
        [MinimumYearValidator]
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1,100000)]
        public uint Quantity { get; set; }
        [Range(1, 100000)]
        public double Price { get; set; }
    }
}
