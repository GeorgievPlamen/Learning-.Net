using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequestRepository>, ILeaveRequestRepository
    {
        private readonly HRLeaveManagementDbContext _dbContext;

        public LeaveRequestRepository(HRLeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LeaveRequest> Add(LeaveRequest entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            leaveRequest.Approved = approvalStatus;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(LeaveRequest entity)
        {
            _dbContext.Set<LeaveRequest>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include (q => q.LeaveType)
                .SingleOrDefaultAsync(q => q.Id == id);
            return leaveRequest ?? throw new ArgumentNullException("Invalid id", nameof(leaveRequest));
        }

        public async Task Update(LeaveRequest entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        async Task<LeaveRequest> IGenericRepository<LeaveRequest>.Get(int id)
        {
            return await _dbContext.Set<LeaveRequest>().FindAsync(id) ?? throw new ArgumentNullException();

        }

        async Task<IReadOnlyList<LeaveRequest>> IGenericRepository<LeaveRequest>.GetAll()
        {
            return await _dbContext.Set<LeaveRequest>().ToListAsync();
        }
    }
}
