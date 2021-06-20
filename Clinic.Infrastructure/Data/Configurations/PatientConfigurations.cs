using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.IdPerson)
                .IsRequired();

            builder.Property(p => p.EntityStatus)
                .IsRequired()
                .HasConversion(x => x.ToString(),
                               x => (EntityStatus)Enum.Parse(typeof(EntityStatus), x));

            builder.Property(p => p.PatientStatus)
                .IsRequired()
                .HasConversion(x => x.ToString(),
                               x => (PatientStatus)Enum.Parse(typeof(PatientStatus), x));

            builder.HasMany<Appointment>(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.IdPacient);

            builder.HasOne<ClinicalHistory>(p => p.ClinicalHistory)
                 .WithOne(c => c.Patient)
                 .HasForeignKey<ClinicalHistory>(c => c.IdPatient);
        }
    }
}
