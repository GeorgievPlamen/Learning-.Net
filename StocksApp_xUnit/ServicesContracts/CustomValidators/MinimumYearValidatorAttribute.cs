using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value,ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if(date.Year < 2000)
                {
                    return new ValidationResult("Minimum year for orders is 2000");
                }
                else
                {
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return null;
        }
    }
}
