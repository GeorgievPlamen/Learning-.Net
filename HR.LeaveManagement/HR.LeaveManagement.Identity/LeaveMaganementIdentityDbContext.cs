using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Configurations;

namespace HR.LeaveManagement.Identity
{
    public class LeaveMaganementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public LeaveMaganementIdentityDbContext(DbContextOptions<LeaveMaganementIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
