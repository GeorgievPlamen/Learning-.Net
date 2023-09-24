using System.ComponentModel.DataAnnotations;

namespace IntroToModels.Models
{
    public class Product
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int ProductCode { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid double value.")]
        public double Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int  Quantity { get; set; }
    }
}
