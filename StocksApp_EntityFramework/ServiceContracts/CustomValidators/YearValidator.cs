using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CustomValidators
{
    internal class YearValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Date is null");

            DateTime check = DateTime.Parse("01-01-2000");
            if ((DateTime)value < check)
            {
                return new ValidationResult("Date is too old for an order");
            }

            return ValidationResult.Success;
        }
    }
}
