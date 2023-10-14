using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class Helpers
    {
        public static void ModelValidation(object obj)
        {
            List<ValidationResult> valRes = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(
                obj, new ValidationContext(obj),
                new List<ValidationResult>(), true);
            if (!isValid)
            {
                throw new ArgumentException(valRes.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
