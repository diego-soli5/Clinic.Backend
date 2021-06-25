﻿// <auto-generated />
using System;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Clinic.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Clinic.Core.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SMToken")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("AppUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EntityStatus = "Enabled",
                            Password = "contraseña001",
                            Role = "User",
                            UserName = "Usuario001"
                        },
                        new
                        {
                            Id = 2,
                            EntityStatus = "Enabled",
                            Password = "contraseña002",
                            Role = "User",
                            UserName = "Usuario002"
                        },
                        new
                        {
                            Id = 3,
                            EntityStatus = "Disabled",
                            Password = "123",
                            Role = "User",
                            UserName = "1diego321"
                        },
                        new
                        {
                            Id = 4,
                            EntityStatus = "Enabled",
                            Password = "123",
                            Role = "Administrator",
                            UserName = "Karamiko"
                        });
                });

            modelBuilder.Entity("Clinic.Core.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppointmentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdMedic")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicAttentionHour")
                        .HasColumnType("int");

                    b.Property<int>("IdPacient")
                        .HasColumnType("int");

                    b.Property<int?>("MedicAttentionHourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdMedic");

                    b.HasIndex("IdPacient");

                    b.HasIndex("MedicAttentionHourId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("Clinic.Core.Entities.ClinicalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 6, 25, 11, 37, 0, 849, DateTimeKind.Local).AddTicks(4863));

                    b.Property<int>("IdPatient")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPatient")
                        .IsUnique();

                    b.ToTable("ClinicalHistory");
                });

            modelBuilder.Entity("Clinic.Core.Entities.ConsultingRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameIdentifier")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("NameIdentifier")
                        .IsUnique();

                    b.ToTable("ConsultingRoom");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            EntityStatus = "Enabled",
                            NameIdentifier = "Cons-001"
                        });
                });

            modelBuilder.Entity("Clinic.Core.Entities.Diagnostic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 6, 25, 11, 37, 0, 865, DateTimeKind.Local).AddTicks(8845));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdClinicalHistory")
                        .HasColumnType("int");

                    b.Property<string>("Observations")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("IdClinicalHistory");

                    b.ToTable("Diagnostic");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmployeeRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdAppUser")
                        .HasColumnType("int");

                    b.Property<int>("IdPerson")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAppUser")
                        .IsUnique();

                    b.HasIndex("IdPerson")
                        .IsUnique();

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeeRole = "Secretary",
                            EmployeeStatus = "Active",
                            HireDate = new DateTime(2021, 6, 25, 11, 37, 0, 900, DateTimeKind.Local).AddTicks(3416),
                            IdAppUser = 1,
                            IdPerson = 1
                        },
                        new
                        {
                            Id = 2,
                            EmployeeRole = "Secretary",
                            EmployeeStatus = "Active",
                            HireDate = new DateTime(2021, 6, 25, 11, 37, 0, 902, DateTimeKind.Local).AddTicks(9840),
                            IdAppUser = 2,
                            IdPerson = 2
                        },
                        new
                        {
                            Id = 3,
                            EmployeeRole = "Secretary",
                            EmployeeStatus = "Active",
                            HireDate = new DateTime(2021, 6, 25, 11, 37, 0, 903, DateTimeKind.Local).AddTicks(107),
                            IdAppUser = 3,
                            IdPerson = 3
                        },
                        new
                        {
                            Id = 4,
                            EmployeeRole = "Medic",
                            EmployeeStatus = "Active",
                            HireDate = new DateTime(2021, 6, 25, 11, 37, 0, 903, DateTimeKind.Local).AddTicks(294),
                            IdAppUser = 4,
                            IdPerson = 4
                        });
                });

            modelBuilder.Entity("Clinic.Core.Entities.Medic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdConsultingRoom")
                        .HasColumnType("int");

                    b.Property<int>("IdEmployee")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicalSpecialty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdConsultingRoom");

                    b.HasIndex("IdEmployee")
                        .IsUnique();

                    b.HasIndex("IdMedicalSpecialty");

                    b.ToTable("Medic");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            IdConsultingRoom = 4,
                            IdEmployee = 4,
                            IdMedicalSpecialty = 4
                        });
                });

            modelBuilder.Entity("Clinic.Core.Entities.MedicAttentionHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("IdMedicalSchedule")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdMedicalSchedule");

                    b.ToTable("AttentionHour");
                });

            modelBuilder.Entity("Clinic.Core.Entities.MedicalSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdMedic")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdMedic");

                    b.ToTable("MedicalSchedule");
                });

            modelBuilder.Entity("Clinic.Core.Entities.MedicalSpecialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("MedicalSpecialty");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Description = "Hace algo ahí de medicina",
                            EntityStatus = "Enabled",
                            Name = "Medicina General"
                        });
                });

            modelBuilder.Entity("Clinic.Core.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdPerson")
                        .HasColumnType("int");

                    b.Property<string>("PatientStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdPerson")
                        .IsUnique();

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Identification")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("Surnames")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Identification")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "San Jose Costa Rica",
                            Birthdate = new DateTime(2021, 6, 25, 11, 37, 0, 902, DateTimeKind.Local).AddTicks(6142),
                            Email = "usuario001@mail.com",
                            Identification = 1231651631,
                            Names = "Miguel",
                            PhoneNumber = 88556161,
                            Surnames = "Hernandez Corrales"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Cartago Costa Rica",
                            Birthdate = new DateTime(2021, 6, 25, 11, 37, 0, 903, DateTimeKind.Local).AddTicks(45),
                            Email = "usuario002@mail.com",
                            Identification = 1891925,
                            Names = "Susan",
                            PhoneNumber = 8919651,
                            Surnames = "Brenes Ilama"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Santa Elena abajo, barrio la Rosa.",
                            Birthdate = new DateTime(1999, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "1diego321@mail.com",
                            Identification = 117310010,
                            Names = "Luis Diego",
                            PhoneNumber = 83358092,
                            Surnames = "Solis Camacho"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Santa Elena Abajo de San Jose Costa Rica",
                            Birthdate = new DateTime(1999, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "1221sdsadas@gmail.com",
                            Identification = 988310010,
                            Names = "Martes Trece",
                            PhoneNumber = 159156,
                            Surnames = "Solis Camacho"
                        });
                });

            modelBuilder.Entity("Clinic.Core.Entities.Appointment", b =>
                {
                    b.HasOne("Clinic.Core.Entities.Medic", "Medic")
                        .WithMany("Appointments")
                        .HasForeignKey("IdMedic")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Clinic.Core.Entities.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("IdPacient")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Clinic.Core.Entities.MedicAttentionHour", "MedicAttentionHour")
                        .WithMany()
                        .HasForeignKey("MedicAttentionHourId");

                    b.Navigation("Medic");

                    b.Navigation("MedicAttentionHour");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Clinic.Core.Entities.ClinicalHistory", b =>
                {
                    b.HasOne("Clinic.Core.Entities.Patient", "Patient")
                        .WithOne("ClinicalHistory")
                        .HasForeignKey("Clinic.Core.Entities.ClinicalHistory", "IdPatient")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Diagnostic", b =>
                {
                    b.HasOne("Clinic.Core.Entities.ClinicalHistory", "ClinicalHistory")
                        .WithMany("Diagnostics")
                        .HasForeignKey("IdClinicalHistory")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ClinicalHistory");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Employee", b =>
                {
                    b.HasOne("Clinic.Core.Entities.AppUser", "AppUser")
                        .WithOne("Employee")
                        .HasForeignKey("Clinic.Core.Entities.Employee", "IdAppUser")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Clinic.Core.Entities.Person", "Person")
                        .WithOne("Employee")
                        .HasForeignKey("Clinic.Core.Entities.Employee", "IdPerson")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Medic", b =>
                {
                    b.HasOne("Clinic.Core.Entities.ConsultingRoom", "ConsultingRoom")
                        .WithMany("Medics")
                        .HasForeignKey("IdConsultingRoom")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Clinic.Core.Entities.Employee", "Employee")
                        .WithOne("Medic")
                        .HasForeignKey("Clinic.Core.Entities.Medic", "IdEmployee")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Clinic.Core.Entities.MedicalSpecialty", "MedicalSpecialty")
                        .WithMany("Medics")
                        .HasForeignKey("IdMedicalSpecialty")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ConsultingRoom");

                    b.Navigation("Employee");

                    b.Navigation("MedicalSpecialty");
                });

            modelBuilder.Entity("Clinic.Core.Entities.MedicAttentionHour", b =>
                {
                    b.HasOne("Clinic.Core.Entities.MedicalSchedule", "MedicalSchedule")
                        .WithMany("MedicAttentionHours")
                        .HasForeignKey("IdMedicalSchedule")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("MedicalSchedule");
                });

            modelBuilder.Entity("Clinic.Core.Entities.MedicalSchedule", b =>
                {
                    b.HasOne("Clinic.Core.Entities.Medic", "Medic")
                        .WithMany("MedicalSchedules")
                        .HasForeignKey("IdMedic")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Medic");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Patient", b =>
                {
                    b.HasOne("Clinic.Core.Entities.Person", "Person")
                        .WithOne("Patient")
                        .HasForeignKey("Clinic.Core.Entities.Patient", "IdPerson")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Clinic.Core.Entities.AppUser", b =>
                {
                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Clinic.Core.Entities.ClinicalHistory", b =>
                {
                    b.Navigation("Diagnostics");
                });

            modelBuilder.Entity("Clinic.Core.Entities.ConsultingRoom", b =>
                {
                    b.Navigation("Medics");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Employee", b =>
                {
                    b.Navigation("Medic");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Medic", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("MedicalSchedules");
                });

            modelBuilder.Entity("Clinic.Core.Entities.MedicalSchedule", b =>
                {
                    b.Navigation("MedicAttentionHours");
                });

            modelBuilder.Entity("Clinic.Core.Entities.MedicalSpecialty", b =>
                {
                    b.Navigation("Medics");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("ClinicalHistory");
                });

            modelBuilder.Entity("Clinic.Core.Entities.Person", b =>
                {
                    b.Navigation("Employee");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
