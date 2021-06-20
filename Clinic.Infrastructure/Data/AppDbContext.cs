using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
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

            base.OnModelCreating(modelBuilder);
        }

    }
}
