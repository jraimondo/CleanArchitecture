using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Identity.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {

        

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

                builder.HasData(
                new ApplicationUser
                {
                    Id = "c2711be2-af4c-43a3-8d9b-0791228cf5bd",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Nombre = "Vaxi",
                    Apellido = "Drez",
                    UserName = "vaxidrez",
                    PasswordHash = hasher.HashPassword(null, "VaxiDrez2025$"),
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "f6552dee-24b6-44d0-9d51-17ab3dd10c3d",
                    Email = "juanperez@localhost.com",
                    NormalizedEmail = "juanperez@localhost.com",
                    Nombre = "Juan",
                    Apellido = "Perez",
                    UserName = "juanperez",
                    PasswordHash = hasher.HashPassword(null, "VaxiDrez2025$"),
                    EmailConfirmed = true,
                });

        }
    }
}
