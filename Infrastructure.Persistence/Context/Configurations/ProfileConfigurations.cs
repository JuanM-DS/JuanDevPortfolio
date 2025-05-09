﻿using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.Configurations
{
    public class ProfileConfigurations : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValue("NewId()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ProfesionalTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.GitHubRepositoryUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.ProfileImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.LinkedinUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.CvUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(x => x.ComenntReferences)
                .WithOne(x=>x.Profile)
                .HasForeignKey(x=>x.ProfileId);

            builder.HasMany(x => x.Skills)
                .WithOne(x => x.Profile)
                .HasForeignKey(x => x.ProfileId);

            builder.HasMany(x => x.Experiences)
                .WithOne(x => x.Profile)
                .HasForeignKey(x => x.ProfileId);

            builder.HasMany(x => x.Projects)
                .WithOne(x => x.Profile)
                .HasForeignKey(x => x.ProfileId);

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
