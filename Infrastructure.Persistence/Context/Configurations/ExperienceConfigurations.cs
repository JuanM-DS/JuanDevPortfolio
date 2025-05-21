using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.Configurations
{
	public class ExperienceConfigurations : IEntityTypeConfiguration<WorkExperience>
	{
		public void Configure(EntityTypeBuilder<WorkExperience> builder)
		{
			builder.ToTable("WorkExperience");
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
				.HasConstraintName("FK_ExpDet_WorkExperience");

			builder.HasMany(e => e.TechnologyItems)
					.WithMany()
					.UsingEntity<Dictionary<string, object>>(
						"WorkExperienceTechnology", 
						j => j
							.HasOne<TechnologyItem>()
							.WithMany()
							.HasForeignKey("TechnologyId") 
							.HasConstraintName("FK_WExpTec_Technology"), 
						j => j
							.HasOne<WorkExperience>()
							.WithMany()
							.HasForeignKey("WorkExperienceId") 
							.HasConstraintName("FK_WExpTec_WorkExperience"),
						j => j
							.HasKey("WorkExperienceId", "TechnologyId") 
							.HasName("PK_WExpTechnology") 
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
