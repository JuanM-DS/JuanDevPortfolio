using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.Configurations
{
    public class CommentsReferencesConfigurations : IEntityTypeConfiguration<CommentReferences>
    {
        public void Configure(EntityTypeBuilder<CommentReferences> builder)
        {
            builder.ToTable("Comment_References");
            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValue("NewId()");

            builder.Property(x => x.PersonName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ProfileImageUrl)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(x => x.Comment)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.ProfileId)
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
