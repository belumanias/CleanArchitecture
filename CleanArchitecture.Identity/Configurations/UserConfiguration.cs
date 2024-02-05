using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "da0c928c-752c-407f-a20e-84935af90cd9",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Nombres = "Belu",
                    Apellidos = "Mañas",
                    UserName = "belumanias",
                    NormalizedUserName = "belumanias",
                    PasswordHash = hasher.HashPassword(null, "beluAdmin!"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "164b3cf9-ebcb-4630-b7fd-46093f823737",
                    Email = "tincho@localhost.com",
                    NormalizedEmail = "tincho@localhost.com",
                    Nombres = "Martin",
                    Apellidos = "Lencina",
                    UserName = "martiinmdq",
                    NormalizedUserName = "martiinmdq",
                    PasswordHash = hasher.HashPassword(null, "martiinMdq!"),
                    EmailConfirmed = true
                }
                );
        }
    }
}
