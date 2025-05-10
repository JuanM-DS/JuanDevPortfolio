using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.Configurations
{
	public class ProjectConfigurations : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			builder.ToTable("Projects");
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.IsRequired()
				.HasDefaultValueSql("NewId()");

			builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(500);

			builder.Property(x => x.GitHubRepositoryUrl)
				.IsRequired()
				.HasMaxLength(500);

			builder.Property(x => x.ProfileId)
				.IsRequired();

			builder.HasMany(p => p.TechnologyItems)
				   .WithMany()
				   .UsingEntity<Dictionary<string, object>>(
					   "ProjectTechnology",
					j => j.HasOne<TechnologyItem>()
						   .WithMany()
						   .HasForeignKey("TechnologyId") 
						.HasConstraintName("FK_ProjectTec_Technology"),
					j => j.HasOne<Project>()
						   .WithMany()
						   .HasForeignKey("ProjectId")
						   .HasConstraintName("FK_ProjTechnology_Project"),
					   j =>
					   {
						   j.HasKey("ProjectId", "TechnologyId"); 
						   j.ToTable("ProjectTechnology"); 
						   j.HasIndex("TechnologyId"); 
					   }
				   );

			builder.HasMany(x => x.ProjectImages)
				.WithOne(x => x.Project)
				.HasForeignKey(x => x.ProjectId)
				.HasConstraintName("FK_ProjectImages_Project");

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
