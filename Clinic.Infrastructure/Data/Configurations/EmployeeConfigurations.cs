using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.IdAppUser)
                .IsRequired();

            builder.Property(e => e.IdPerson)
                .IsRequired();

            builder.Property(e => e.EmployeeRole)
                .IsRequired()
                .HasConversion(e => e.ToString(),
                               e => (EmployeeRole)Enum.Parse(typeof(EmployeeRole), e));

            builder.Property(e => e.EmployeeStatus)
                .IsRequired()
                .HasConversion(e => e.ToString(),
                               e => (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), e));

            builder.HasOne<Medic>(e => e.Medic)
                .WithOne(m => m.Employee)
                .HasForeignKey<Medic>(m => m.IdEmployee)
                .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}