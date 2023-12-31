﻿using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = HR.LeaveManagement.Application.Exceptions.ValidationException;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _allocationRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository allocationRepository, IMapper mapper)
        {
            _allocationRepository = allocationRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateLeaveAllocationDTO == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var validator = new UpdateLeaveAllocationDTOValidator(_allocationRepository);
            var validationResult = await validator.ValidateAsync(request.UpdateLeaveAllocationDTO);
            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var leaveAllocation = await _allocationRepository.Get(request.UpdateLeaveAllocationDTO.Id);

            _mapper.Map(request.UpdateLeaveAllocationDTO, leaveAllocation);

            await _allocationRepository.Update(leaveAllocation);

            return Unit.Value;
        }
    }
}
