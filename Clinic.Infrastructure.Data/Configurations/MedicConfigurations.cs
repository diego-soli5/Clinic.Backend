using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class MedicConfigurations : IEntityTypeConfiguration<Medic>
    {
        public void Configure(EntityTypeBuilder<Medic> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.IdConsultingRoom)
                .IsRequired();

            builder.Property(m => m.IdEmployee)
                .IsRequired();

            builder.Property(m => m.IdMedicalSpecialty)
                .IsRequired();

            builder.HasMany<MedicalSchedule>(med => med.MedicalSchedules)
                .WithOne(medSch => medSch.Medic)
                .HasForeignKey(medSch => medSch.IdMedic)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany<Appointment>(m => m.Appointments)
                .WithOne(a => a.Medic)
                .HasForeignKey(a => a.IdMedic)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
