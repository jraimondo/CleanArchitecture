using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "29ae0766-2113-4869-bf03-44e11ba3845b",
                    UserId = "c2711be2-af4c-43a3-8d9b-0791228cf5bd"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "0430b079-d867-40e9-8b3d-02735f35f5d9",
                    UserId = "f6552dee-24b6-44d0-9d51-17ab3dd10c3d"
                });
        }
    }
}
