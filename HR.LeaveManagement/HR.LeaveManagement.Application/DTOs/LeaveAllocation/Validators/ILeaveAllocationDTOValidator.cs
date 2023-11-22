using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDTOValidator : AbstractValidator<ILeaveAllocationDTO>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public ILeaveAllocationDTOValidator(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;

            RuleFor(p => p.NumberOfDays)
                .NotEmpty().WithMessage("{PropertyName} has to be filled out.")
                .GreaterThan(0).WithMessage("{PropertyName} has to be atleast 1.")
                .LessThan(100).WithMessage("{PropertyName} has to be below 100.");

            RuleFor(p => p.LeaveType)
                .NotEmpty().WithMessage("{PropertyName} has to be filled out.")
                .MustAsync(async (id, token) =>
                {
                    if (id?.Id == null)
                    {
                        throw new ArgumentNullException(nameof(id.Id));
                    }
                    var leaveTypeExists = await _leaveAllocationRepository.Exists(id.Id);
                    return !leaveTypeExists;
                });

            RuleFor(p => p.Period)
                .GreaterThan(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");
        }
    }
}

