using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var leaveAllocation = await _allocationRepository.Get(request.UpdateLeaveAllocationDTO.Id);

            _mapper.Map(request.UpdateLeaveAllocationDTO, leaveAllocation);

            await _allocationRepository.Update(leaveAllocation);

            return Unit.Value;
        }
    }
}
