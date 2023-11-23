using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequestRepository>, ILeaveRequestRepository
    {
        private readonly HRLeaveManagementDbContext _dbContext;

        public LeaveRequestRepository(HRLeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<LeaveRequest> Add(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            throw new NotImplementedException();
        }

        public Task Delete(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }

        Task<LeaveRequest> IGenericRepository<LeaveRequest>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<LeaveRequest>> IGenericRepository<LeaveRequest>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
