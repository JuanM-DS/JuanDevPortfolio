using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class MainContext : DbContext
    {
        public DbSet<CommentReference> CommentReferences { get; set; }
        public DbSet<WorkExperience> Experiences { get; set; }
        public DbSet<WorkExperienceDetail> ExperienceDetails { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TechnologyItem> TechnologyItems { get; set; }

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
        }
    }
}
