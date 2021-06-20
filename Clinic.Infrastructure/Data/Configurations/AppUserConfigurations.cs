using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(a => a.UserName)
                .IsUnique();

            builder.Property(a => a.Password)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Role)
                .IsRequired()
                .HasConversion(a => a.ToString(),
                               a => (AppUserRole)Enum.Parse(typeof(AppUserRole), a));

            builder.Property(a => a.EntityStatus)
                .IsRequired()
                .HasConversion(a => a.ToString(),
                               a => (EntityStatus)Enum.Parse(typeof(EntityStatus), a));

            builder.HasOne<Employee>(a => a.Employee)
                .WithOne(e => e.AppUser)
                .HasForeignKey<Employee>(e => e.IdAppUser);
        }
    }
}
