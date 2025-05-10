using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.Configurations
{
    public class ProjectImageConfigurations : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> builder)
        {
            builder.ToTable("Project_Images");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NewId()");

            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

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
