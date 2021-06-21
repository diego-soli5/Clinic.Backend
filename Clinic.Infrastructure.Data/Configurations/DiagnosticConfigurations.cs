using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class DiagnosticConfigurations : IEntityTypeConfiguration<Diagnostic>
    {
        public void Configure(EntityTypeBuilder<Diagnostic> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.Observations)
                .HasMaxLength(500);

            builder.Property(d => d.IdClinicalHistory)
                .IsRequired();

            builder.Property(d => d.CreatedDate)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.Property(d => d.EntityStatus)
                .IsRequired()
                .HasConversion(a => a.ToString(),
                               a => (EntityStatus)Enum.Parse(typeof(EntityStatus), a));
        }
    }
}
