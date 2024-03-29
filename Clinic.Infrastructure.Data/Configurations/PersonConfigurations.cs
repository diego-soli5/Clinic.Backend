﻿using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class PersonConfigurations : IEntityTypeConfiguration<Person>
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

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.Property(p => p.Address)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .IsRequired();
            builder.HasIndex(p => p.PhoneNumber)
                .IsUnique();

            builder.Property(p => p.Birthdate)
                .IsRequired();

            builder.Property(p => p.ImageName)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne<Employee>(p => p.Employee)
                .WithOne(e => e.Person)
                .HasForeignKey<Employee>(e => e.IdPerson)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Patient>(pe => pe.Patient)
                .WithOne(pa => pa.Person)
                .HasForeignKey<Patient>(pa => pa.IdPerson)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
