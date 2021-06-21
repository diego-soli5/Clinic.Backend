using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class MedicalSpecialtyConfigurations : IEntityTypeConfiguration<MedicalSpecialty>
    {
        public void Configure(EntityTypeBuilder<MedicalSpecialty> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder.HasIndex(m => m.Name).IsUnique();

            builder.Property(m => m.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.EntityStatus)
                .IsRequired()
                .HasConversion(x => x.ToString(),
                               x => (EntityStatus)Enum.Parse(typeof(EntityStatus), x));

            builder.HasMany<Medic>(m => m.Medics)
                .WithOne(m => m.MedicalSpecialty)
                .HasForeignKey(m => m.IdMedicalSpecialty)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
