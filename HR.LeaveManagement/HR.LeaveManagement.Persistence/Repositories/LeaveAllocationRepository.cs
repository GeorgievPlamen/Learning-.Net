using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocationRepository>, ILeaveAllocationRepository
    {
        private readonly HRLeaveManagementDbContext _dbContext;

        public LeaveAllocationRepository(HRLeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<LeaveAllocation> Add(LeaveAllocation entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(LeaveAllocation entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(LeaveAllocation entity)
        {
            throw new NotImplementedException();
        }

        Task<LeaveAllocation> IGenericRepository<LeaveAllocation>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<LeaveAllocation>> IGenericRepository<LeaveAllocation>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
