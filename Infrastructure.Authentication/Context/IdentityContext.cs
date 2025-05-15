using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Context
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public override DbSet<AppUser> Users { get; set; } = null!;
        public override DbSet<AppRole> Roles { get; set; } = null!;

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
        }
    }
}
