using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "29ae0766-2113-4869-bf03-44e11ba3845b",
                    Name = "Administrator",
                    NormalizedName="ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "0430b079-d867-40e9-8b3d-02735f35f5d9",
                    Name = "Operator",
                    NormalizedName = "OPERATOR"
                }
                ) ;
        }
    }
}
