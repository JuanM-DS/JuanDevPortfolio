using Infrastructure.Authentication.CustomEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Authentication.Context.Configurations
{
	public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
	{
		public void Configure(EntityTypeBuilder<RefreshToken> builder)
		{
			builder.ToTable("RefreshToken");
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.IsRequired();

			builder.Property(x => x.Token)
				.IsRequired();

			builder.Property(x => x.UserId)
				.IsRequired();

			builder.Property(x => x.LifeTime)
				.IsRequired()
				.HasColumnType("date");

			builder.Property(x => x.IsActive)
				.IsRequired()
				.HasColumnType("bit");

			builder.Property(x => x.IpAddress)
				.IsRequired();

			builder.HasIndex(x => x.IpAddress)
				.IsUnique();
			builder.HasIndex(x => x.Token)
				.IsUnique();
			builder.HasIndex(x => x.UserId);
			builder.HasIndex(x => x.IsActive);

			builder.HasOne(x => x.User)
				.WithMany()
				.HasForeignKey(x => x.UserId);

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
		}
	}
}
