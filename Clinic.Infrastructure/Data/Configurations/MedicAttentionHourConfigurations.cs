using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class MedicAttentionHourConfigurations : IEntityTypeConfiguration<MedicAttentionHour>
    {
        public void Configure(EntityTypeBuilder<MedicAttentionHour> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Hour)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(m => m.IdMedicalSchedule)
                .IsRequired();

            builder.HasOne<MedicalSchedule>(medAtt => medAtt.MedicalSchedule)
                .WithMany(medSch => medSch.MedicAttentionHours)
                .HasForeignKey(medSch => medSch.IdMedicalSchedule);

            builder.Property(a => a.EntityStatus)
                .IsRequired()
                .HasConversion(a => a.ToString(),
                               a => (EntityStatus)Enum.Parse(typeof(EntityStatus), a));
        }
    }
}
