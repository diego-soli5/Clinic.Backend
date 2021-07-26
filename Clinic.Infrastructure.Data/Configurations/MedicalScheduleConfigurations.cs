using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class MedicalScheduleConfigurations : IEntityTypeConfiguration<MedicalSchedule>
    {
        public void Configure(EntityTypeBuilder<MedicalSchedule> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.DayOfWeek)
                .IsRequired()
                .HasConversion(x => x.ToString(),
                               x => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), x));

            builder.Property(m => m.IdMedic)
                .IsRequired();

            builder.Property(m => m.EntityStatus)
                .IsRequired()
                .HasConversion(x => x.ToString(),
                               x => (EntityStatus)Enum.Parse(typeof(EntityStatus), x));

            builder.HasMany<MedicAttentionHour>(medSch => medSch.MedicAttentionHours)
                .WithOne(medAtt => medAtt.MedicalSchedule)
                .HasForeignKey(medAtt => medAtt.IdMedicalSchedule)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
