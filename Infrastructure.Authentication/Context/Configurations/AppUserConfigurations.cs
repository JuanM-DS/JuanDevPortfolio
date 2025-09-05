using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Authentication.Context.Configurations
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ProfileImageUrl)
                .IsRequired();

            builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(50);

			#region AuditableProperties
			builder.Property(x => x.CreatedBy)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.Created)
				.IsRequired();

			builder.Property(x => x.UpdatedBy)
				.IsRequired(false)
				.HasMaxLength(200);

			builder.Property(x => x.Updated)
				.IsRequired(false);
			#endregion

			builder.HasMany(x => x.Roles)
				.WithMany(f=>f.Users)
				.UsingEntity<IdentityUserRole<Guid>>();
		}
	}
}
