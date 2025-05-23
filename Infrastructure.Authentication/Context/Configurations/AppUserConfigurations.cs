using Infrastructure.Authentication.CustomEntities;
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
		}
    }
}
