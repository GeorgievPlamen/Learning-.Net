using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "DD7ADC5D-4B30-4C56-B198-520F02C00AF8",
                    UserId = "29D82DD3-6198-4917-BD71-28190B056BB3"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "6547AA43-820C-47AE-9871-53A24517073C",
                    UserId = "14F0B273-522B-42D3-B636-9E08B6EB0BFA"
                }
                );
        }
    }
}
