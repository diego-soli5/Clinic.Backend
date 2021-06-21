using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Clinic.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<ClinicalHistory> ClinicalHistory { get; set; }
        public DbSet<ConsultingRoom> ConsultingRoom { get; set; }
        public DbSet<Diagnostic> Diagnostic { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Medic> Medic { get; set; }
        public DbSet<MedicalSchedule> MedicalSchedule { get; set; }
        public DbSet<MedicalSpecialty> MedicalSpecialty { get; set; }
        public DbSet<MedicAttentionHour> AttentionHour { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                HireDate = DateTime.Now,
                EmployeeRole = EmployeeRole.Secretary,
                EmployeeStatus = EmployeeStatus.Active,
                Id = 1,
                IdAppUser = 1,
                IdPerson = 1
            });

            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                UserName = "Usuario001",
                Password = "contraseña001",
                EntityStatus = EntityStatus.Enabled,
                Role = AppUserRole.User,
                Id = 1
            });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                Address = "San Jose Costa Rica",
                Birthdate = DateTime.Now,
                Email = "usuario001@mail.com",
                Identification = 1231651631,
                Names = "Miguel",
                PhoneNumber = 88556161,
                ImageName = null,
                Surnames = "Hernandez Corrales",
                Id = 1
            });

            //////////////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                HireDate = DateTime.Now,
                EmployeeRole = EmployeeRole.Secretary,
                EmployeeStatus = EmployeeStatus.Active,
                Id = 2,
                IdAppUser = 2,
                IdPerson = 2
            });

            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                UserName = "Usuario002",
                Password = "contraseña002",
                EntityStatus = EntityStatus.Enabled,
                Role = AppUserRole.User,
                Id = 2
            });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                Address = "Cartago Costa Rica",
                Birthdate = DateTime.Now,
                Email = "usuario002@mail.com",
                Identification = 1891925,
                Names = "Susan",
                PhoneNumber = 8919651,
                ImageName = null,
                Surnames = "Brenes Ilama",
                Id = 2
            });

            //////////////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                HireDate = DateTime.Now,
                EmployeeRole = EmployeeRole.Secretary,
                EmployeeStatus = EmployeeStatus.Fired,
                Id = 3,
                IdAppUser = 3,
                IdPerson = 3
            });

            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                UserName = "Usuario003",
                Password = "contraseña003",
                EntityStatus = EntityStatus.Disabled,
                Role = AppUserRole.User,
                Id = 3
            });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                Address = "Limon Costa Rica",
                Birthdate = DateTime.Now,
                Email = "usuario003@mail.com",
                Identification = 298498198,
                Names = "Carlos",
                PhoneNumber = 865498198,
                ImageName = null,
                Surnames = "Rojas Salas",
                Id = 3
            });

            //////////////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                HireDate = DateTime.Now,
                EmployeeRole = EmployeeRole.Medic,
                EmployeeStatus = EmployeeStatus.Active,
                Id = 4,
                IdAppUser = 4,
                IdPerson = 4
            });

            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                UserName = "1diego321",
                Password = "123",
                EntityStatus = EntityStatus.Enabled,
                Role = AppUserRole.Administrator,
                Id = 4
            });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                Address = "Santa Elena Abajo de San Jose Costa Rica",
                Birthdate = new DateTime(1999, 1, 3),
                Email = "1diego321@gmail.com",
                Identification = 117310010,
                Names = "Luis Diego",
                PhoneNumber = 83358092,
                ImageName = null,
                Surnames = "Solis Camacho",
                Id = 4
            });

            modelBuilder.Entity<Medic>().HasData(new Medic
            {
                IdConsultingRoom = 4,
                IdEmployee = 4,
                IdMedicalSpecialty = 4,
                Id = 4
            });

            modelBuilder.Entity<ConsultingRoom>().HasData(new ConsultingRoom
            {
                EntityStatus = EntityStatus.Enabled,
                NameIdentifier = "Cons-001",
                Id = 4
            });

            modelBuilder.Entity<MedicalSpecialty>().HasData(new MedicalSpecialty
            {
                Description = "Hace algo ahí de medicina",
                EntityStatus = EntityStatus.Enabled,
                Name = "Medicina General",
                Id = 4
            });
        }
    }
}
