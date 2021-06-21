using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class ClinicalHistoryConfigurations : IEntityTypeConfiguration<ClinicalHistory>
    {
        public void Configure(EntityTypeBuilder<ClinicalHistory> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IdPatient)
                .IsRequired();

            builder.Property(c => c.CreationDate)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.HasMany(c => c.Diagnostics)
                .WithOne(d => d.ClinicalHistory)
                .HasForeignKey(d => d.IdClinicalHistory)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
