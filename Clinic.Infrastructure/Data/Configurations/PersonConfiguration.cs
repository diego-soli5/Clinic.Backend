using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Identification)
                .IsRequired();

            builder.HasIndex(p => p.Identification)
                .IsUnique();

            builder.Property(p => p.Names)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Surnames)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Address)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .IsRequired();

            builder.Property(p => p.Birthdate)
                .IsRequired();

            builder.Property(p => p.ImageName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.HasOne<Employee>(p => p.Employee)
                .WithOne(e => e.Person)
                .HasForeignKey<Employee>(e => e.IdPerson);

            builder.HasOne<Patient>(pe => pe.Patient)
                .WithOne(pa => pa.Person)
                .HasForeignKey<Patient>(pa => pa.IdPerson);
        }
    }
}
