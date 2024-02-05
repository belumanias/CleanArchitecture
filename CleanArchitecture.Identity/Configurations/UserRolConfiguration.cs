using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRolConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string> 
                { 
                    RoleId = "1520bb83-e11f-4f43-a062-8accebb90138",
                    UserId = "da0c928c-752c-407f-a20e-84935af90cd9"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "4d7cb737-31c1-452d-bcfb-d7df73addb6f",
                    UserId = "164b3cf9-ebcb-4630-b7fd-46093f823737"
                }
                );
        }
    }
}
