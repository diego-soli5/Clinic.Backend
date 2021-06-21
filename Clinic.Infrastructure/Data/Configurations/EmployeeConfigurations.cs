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

            builder.HasData(new[]
            {
                new Employee
                {
                    AppUser = new AppUser
                    {
                        UserName= "Usuario001",
                        Password = "contraseña001",
                        EntityStatus = EntityStatus.Enabled,
                        Role = AppUserRole.User
                    },
                    Person = new Person
                    {
                        Address = "San Jose Costa Rica",
                        Birthdate = DateTime.Now,
                        Email = "usuario001@mail.com",
                        Identification = 1231651631,
                        Names = "Miguel",
                        PhoneNumber = 88556161,
                        ImageName = null,
                        Surnames = "Hernandez Corrales"
                    },
                    HireDate = DateTime.Now,
                    EmployeeRole = EmployeeRole.Secretary,
                    EmployeeStatus = EmployeeStatus.Active
                },

                new Employee
                {
                    AppUser = new AppUser
                    {
                        UserName= "Usuario002",
                        Password = "contraseña002",
                        EntityStatus = EntityStatus.Enabled,
                        Role = AppUserRole.User
                    },
                    Person = new Person
                    {
                        Address = "Cartago Costa Rica",
                        Birthdate = DateTime.Now,
                        Email = "usuario002@mail.com",
                        Identification = 1891925,
                        Names = "Susan",
                        PhoneNumber = 8919651,
                        ImageName = null,
                        Surnames = "Brenes Ilama"
                    },
                    HireDate = DateTime.Now,
                    EmployeeRole = EmployeeRole.Secretary,
                    EmployeeStatus = EmployeeStatus.Active
                },
                new Employee
                {
                    AppUser = new AppUser
                    {
                        UserName= "Usuario003",
                        Password = "contraseña003",
                        EntityStatus = EntityStatus.Disabled,
                        Role = AppUserRole.User
                    },
                    Person = new Person
                    {
                        Address = "Limon Costa Rica",
                        Birthdate = DateTime.Now,
                        Email = "usuario003@mail.com",
                        Identification = 298498198,
                        Names = "Carlos",
                        PhoneNumber = 865498198,
                        ImageName = null,
                        Surnames = "Rojas Salas"
                    },
                    HireDate = DateTime.Now,
                    EmployeeRole = EmployeeRole.Secretary,
                    EmployeeStatus = EmployeeStatus.Fired
                },

                new Employee
                {
                    AppUser = new AppUser
                    {
                        UserName= "1diego321",
                        Password = "123",
                        EntityStatus = EntityStatus.Enabled,
                        Role = AppUserRole.Administrator
                    },
                    Person = new Person
                    {
                        Address = "Santa Elena Abajo de San Jose Costa Rica",
                        Birthdate = new DateTime(1999,1,3),
                        Email = "1diego321@gmail.com",
                        Identification = 117310010,
                        Names = "Luis Diego",
                        PhoneNumber = 83358092,
                        ImageName = null,
                        Surnames = "Solis Camacho"
                    },
                    Medic = new Medic
                    {
                        ConsultingRoom = new ConsultingRoom
                        {
                            EntityStatus = EntityStatus.Enabled,
                            NameIdentifier = "Cons-001",
                        },
                        MedicalSpecialty = new MedicalSpecialty
                        {
                            Description = "Hace algo ahí de medicina",
                            EntityStatus = EntityStatus.Enabled,
                            Name = "Medicina General"
                        }
                    },
                    HireDate = DateTime.Now,
                    EmployeeRole = EmployeeRole.Medic,
                    EmployeeStatus = EmployeeStatus.Active
                }
            });
        }
    }
}
