using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = new Guid("27aec839-5fa7-4dab-8295-c5fb64dd0c64").ToString(),
                    Name = "SuperAdministrator",
                    NormalizedName = "SUPERADMINISTRATOR",
                    ConcurrencyStamp = "0a756968-67bd-4e6e-9863-bd823cf36619"
                },
                new IdentityRole
                {
                    Id = new Guid("44e6e525-9fc7-46e7-99e3-9ce309c664ff").ToString(),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "677e6ef6-a7eb-4143-91b2-37a858ae1f16"
                },
                new IdentityRole
                {
                    Id = new Guid("8f769fa5-0753-4772-82c3-39dd1dc5759b").ToString(),
                    Name = "Employer",
                    NormalizedName = "EMPLOYER",
                    ConcurrencyStamp = "553cdc39-d6c6-4b7a-90c2-0cee1a1834f8",
                },
                new IdentityRole
                {
                    Id = new Guid("8f2f0ddf-3d09-4ec2-93ab-e2f7af6db872").ToString(),
                    Name = "Applicant",
                    NormalizedName = "APPLICANT",
                    ConcurrencyStamp = "53969479-15ea-4be7-9e63-bdb02ff195d7"
                });
        }
    }
}
