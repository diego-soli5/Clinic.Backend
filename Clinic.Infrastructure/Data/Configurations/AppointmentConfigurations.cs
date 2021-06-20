using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.IdMedicAttentionHour)
                .IsRequired();

            builder.Property(a => a.IdMedic)
               .IsRequired();

            builder.Property(a => a.IdPacient)
               .IsRequired();

            builder.Property(a => a.Date)
               .IsRequired();

            builder.Property(a => a.AppointmentStatus)
               .IsRequired()
               .HasConversion(a => a.ToString(),
                               a => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), a));

        }
    }
}
