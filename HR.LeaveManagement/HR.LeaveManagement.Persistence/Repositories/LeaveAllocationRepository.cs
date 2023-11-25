using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Contracts.Persistence;
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
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocationRepository>, ILeaveAllocationRepository
    {
        private readonly HRLeaveManagementDbContext _dbContext;

        public LeaveAllocationRepository(HRLeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LeaveAllocation> Add(LeaveAllocation entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(LeaveAllocation entity)
        {
            _dbContext.Set<LeaveAllocation>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = _dbContext.LeaveAllocations
                .Include(x => x.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _dbContext.LeaveAllocations
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);
            return leaveAllocation ?? throw new ArgumentNullException("Invalid id",nameof(leaveAllocation));
        }

        public async Task Update(LeaveAllocation entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        async Task<LeaveAllocation> IGenericRepository<LeaveAllocation>.Get(int id)
        {
            return await _dbContext.Set<LeaveAllocation>().FindAsync(id) ?? throw new ArgumentNullException();

        }

        async Task<IReadOnlyList<LeaveAllocation>> IGenericRepository<LeaveAllocation>.GetAll()
        {
            return await _dbContext.Set<LeaveAllocation>().ToListAsync();
        }
    }
}
