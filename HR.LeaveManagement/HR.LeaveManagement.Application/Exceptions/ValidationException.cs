using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string>? Errors { get; set; } = new List<string>();

        public ValidationException(ValidationResult validationResult)
        {
            if (validationResult.Errors != null)
            {
                foreach (var error in validationResult.Errors)
                {
                    Errors.Add(error.ErrorMessage);
                }
            }
        }
    }
}
