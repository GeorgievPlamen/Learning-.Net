using Orders.WebAPI.Enteties.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.WebAPI.Enteties
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string? ProductName { get; set; }
        [PositiveNumber]
        public int Quantity { get; set; }
        [PositiveNumber]
        public double? UnitPrice { get; set; }
        [PositiveNumber]
        public double? TotalPrice { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

    }
}
