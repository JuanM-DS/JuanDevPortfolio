using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.Configurations
{
    public class ExperienceDetailConfigurations : IEntityTypeConfiguration<ExperienceDetail>
    {
        public void Configure(EntityTypeBuilder<ExperienceDetail> builder)
        {
            builder.ToTable("Experience_Details");
            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValue("NewId()");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.ExperienceId)
                .IsRequired();

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
