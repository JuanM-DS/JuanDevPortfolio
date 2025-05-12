using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Infrastructure.Persistence.Context.Configurations
{
	public class ExperienceConfigurations : IEntityTypeConfiguration<Experience>
	{
		public void Configure(EntityTypeBuilder<Experience> builder)
		{
			builder.ToTable("Experiences");
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.IsRequired()
				.HasDefaultValueSql("NewId()");

			builder.Property(x => x.Position)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.CompanyName)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.FromDate)
				.IsRequired()
				.HasColumnType("Date");

			builder.Property(x => x.ToDate)
				.IsRequired(false)
				.HasColumnType("Date");

			builder.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(500);

			builder.Property(x => x.ImageUrl)
				.IsRequired()
				.HasMaxLength(500);

			builder.Property(x => x.ProfileId)
				.IsRequired();

			builder.HasMany(x => x.ExperienceDetails)
				.WithOne(x => x.Experience)
				.HasForeignKey(x => x.ExperienceId)
				.HasConstraintName("FK_ExpDet_Experience");

			builder.HasMany(e => e.TechnologyItems)
					.WithMany()
					.UsingEntity<Dictionary<string, object>>(
						"ExperienceTechnology", 
						j => j
							.HasOne<TechnologyItem>()
							.WithMany()
							.HasForeignKey("TechnologyId") 
							.HasConstraintName("FK_ExpTec_Technology"), 
						j => j
							.HasOne<Experience>()
							.WithMany()
							.HasForeignKey("ExperienceId") 
							.HasConstraintName("FK_ExpTec_Experience"),
						j => j
							.HasKey("ExperienceId", "TechnologyId") 
							.HasName("PK_ExpTechnology") 
					);

			#region AuditableProperties
			builder.Property(x => x.CreatedBy)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.Created)
				.IsRequired();

			builder.Property(x => x.UpdatedBy)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.Updated)
				.IsRequired();
			#endregion
		}
	}
}
