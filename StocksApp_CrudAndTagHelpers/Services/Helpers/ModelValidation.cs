using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    internal static class ModelValidation 
    {
        internal static void Validate(object obj)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), errors);
            if (!isValid)
            {
                throw new ArgumentException(errors.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
