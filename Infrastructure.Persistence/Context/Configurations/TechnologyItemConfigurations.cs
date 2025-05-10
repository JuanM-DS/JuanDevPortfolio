using Core.Domain.Entities;
using Core.Domain.Enumerables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.Configurations
{
    public class TechnologyItemConfigurations : IEntityTypeConfiguration<TechnologyItem>
    {
        public void Configure(EntityTypeBuilder<TechnologyItem> builder)
        {
            builder.ToTable("Technology_Items");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NewId()");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ImageIconUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.LevelType)
                .IsRequired()
                .HasConversion(
                x => x.ToString(),
                x => (LevelsTypes)Enum.Parse(typeof(LevelsTypes), x)
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
