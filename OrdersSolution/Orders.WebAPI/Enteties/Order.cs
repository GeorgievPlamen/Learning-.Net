using Orders.WebAPI.Enteties.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebAPI.Enteties
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public string? OrderNumber { get; set; }
        [Required(ErrorMessage = "Customer name is required"),MaxLength(50)]
        public string? CustomerName { get; set; }
        [Required(ErrorMessage = "Date for order is required")]
        public DateTime OrderDate { get; set; }
        [PositiveNumber]
        public double TotalAmount { get; set; }
       
    }
}
