using System.ComponentModel.DataAnnotations;
using IntroToModels.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IntroToModels.Models
{
    public class Order : IValidatableObject
    {
        [BindNever]
        public int? OrderNo { get; set; }
        [Required]
        [MinimumYearValidator]
        public DateTime OrderDate { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid double value.")]
        public double InvoicePrice { get; set; }
        [Required]
        public List<Product> Products { get; set; } = new List<Product>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Products.Count < 1)
            {
                yield return new ValidationResult("There has to be atleast one product per order");
            }
            double _price = 0;
            foreach(Product product in Products)
            {
                _price += product.Price * product.Quantity;
            }
            if(InvoicePrice != _price)
            {
                yield return new ValidationResult("InvoicePrice doesn't match with the total cost of the specified products in the order.");
            }
        }
    }
}
