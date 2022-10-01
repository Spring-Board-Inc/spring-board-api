using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Configurations;

namespace Repositories
{
    public class RepositoryContext : IdentityDbContext<AppUser>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<UserSkill>()
                .HasKey(us => new { us.UserInformationId, us.SkillId });
            modelBuilder.Entity<UserJob>()
                .HasKey(uj => new { uj.JobId, uj.UserId});
        }

        public DbSet<Certification>? Certifications { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Education>? Educations { get; set; }
        public DbSet<Industry>? Industries { get; set; }
        public DbSet<Job>? Jobs { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Skill>? Skills { get; set; }
        public DbSet<JobType>? JobTypes { get; set; }
        public DbSet<UserInformation>? UserInformation { get; set; }
        public DbSet<WorkExperience>? WorkExperiences { get; set; }
        public DbSet<Token>? Tokens { get; set; }
        public DbSet<UserSkill>? UserSkills { get; set; }
        public DbSet<UserJob>? UserJobs { get; set; }
    }
}
