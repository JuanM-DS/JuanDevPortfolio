﻿using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.Configurations
{
    public class SkillsConfigurations : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skills");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NewId()");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.ProfileId)
                .IsRequired();

			builder.HasMany(s => s.TechnologyItems)
					.WithMany()
					.UsingEntity<Dictionary<string, object>>(
						"SkillTechnology",
						j => j.HasOne<TechnologyItem>()
							 .WithMany()
							 .HasForeignKey("TechnologyId")
							 .HasConstraintName("FK_SkillTec_Technology"),
						j => j.HasOne<Skill>()
							 .WithMany()
							 .HasForeignKey("SkillId")
							 .HasConstraintName("FK_SkillTec_Skill"),
						j => {
							j.HasKey("SkillId", "TechnologyId");
							j.ToTable("SkillTechnology");
							j.HasIndex("TechnologyId");
						}
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
