using System.ComponentModel.DataAnnotations;

namespace IntroToModels.CustomValidators
{
    public class MinimumYearValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if(date.Year >= 2000)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Minimum year allowed for orders is 2000.");
                }
            }

            return new ValidationResult("Date of order has to be provided.");
        }
    }
}
