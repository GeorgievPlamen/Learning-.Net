using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebAPI.Enteties.CustomValidation
{
    public class PositiveNumber : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int)
            {
                if ((int)value > 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Value must be positive");
        }
    }
}
