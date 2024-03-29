﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Infrastructure.Data.Migrations
{
    public partial class AllTableEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMToken = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsultingRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameIdentifier = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultingRoom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalSpecialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalSpecialty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identification = table.Column<int>(type: "int", nullable: false),
                    Names = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surnames = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAppUser = table.Column<int>(type: "int", nullable: false),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_AppUser_IdAppUser",
                        column: x => x.IdAppUser,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdConsultingRoom = table.Column<int>(type: "int", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    IdMedicalSpecialty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medic_ConsultingRoom_IdConsultingRoom",
                        column: x => x.IdConsultingRoom,
                        principalTable: "ConsultingRoom",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medic_Employee_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medic_MedicalSpecialty_IdMedicalSpecialty",
                        column: x => x.IdMedicalSpecialty,
                        principalTable: "MedicalSpecialty",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClinicalHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPatient = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 7, 25, 19, 30, 8, 647, DateTimeKind.Local).AddTicks(3545))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicalHistory_Patient_IdPatient",
                        column: x => x.IdPatient,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMedic = table.Column<int>(type: "int", nullable: false),
                    EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalSchedule_Medic_IdMedic",
                        column: x => x.IdMedic,
                        principalTable: "Medic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Diagnostic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IdClinicalHistory = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 7, 25, 19, 30, 8, 664, DateTimeKind.Local).AddTicks(2210)),
                    EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnostic_ClinicalHistory_IdClinicalHistory",
                        column: x => x.IdClinicalHistory,
                        principalTable: "ClinicalHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttentionHour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IdMedicalSchedule = table.Column<int>(type: "int", nullable: false),
                    EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttentionHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttentionHour_MedicalSchedule_IdMedicalSchedule",
                        column: x => x.IdMedicalSchedule,
                        principalTable: "MedicalSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMedicAttentionHour = table.Column<int>(type: "int", nullable: false),
                    IdMedic = table.Column<int>(type: "int", nullable: false),
                    IdPacient = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicAttentionHourId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_AttentionHour_MedicAttentionHourId",
                        column: x => x.MedicAttentionHourId,
                        principalTable: "AttentionHour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_Medic_IdMedic",
                        column: x => x.IdMedic,
                        principalTable: "Medic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointment_Patient_IdPacient",
                        column: x => x.IdPacient,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_IdMedic",
                table: "Appointment",
                column: "IdMedic");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_IdPacient",
                table: "Appointment",
                column: "IdPacient");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_MedicAttentionHourId",
                table: "Appointment",
                column: "MedicAttentionHourId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_UserName",
                table: "AppUser",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttentionHour_IdMedicalSchedule",
                table: "AttentionHour",
                column: "IdMedicalSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalHistory_IdPatient",
                table: "ClinicalHistory",
                column: "IdPatient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsultingRoom_NameIdentifier",
                table: "ConsultingRoom",
                column: "NameIdentifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostic_IdClinicalHistory",
                table: "Diagnostic",
                column: "IdClinicalHistory");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IdAppUser",
                table: "Employee",
                column: "IdAppUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IdPerson",
                table: "Employee",
                column: "IdPerson",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medic_IdConsultingRoom",
                table: "Medic",
                column: "IdConsultingRoom");

            migrationBuilder.CreateIndex(
                name: "IX_Medic_IdEmployee",
                table: "Medic",
                column: "IdEmployee",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medic_IdMedicalSpecialty",
                table: "Medic",
                column: "IdMedicalSpecialty");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalSchedule_IdMedic",
                table: "MedicalSchedule",
                column: "IdMedic");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalSpecialty_Name",
                table: "MedicalSpecialty",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_IdPerson",
                table: "Patient",
                column: "IdPerson",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_Email",
                table: "Person",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_Identification",
                table: "Person",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_PhoneNumber",
                table: "Person",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Diagnostic");

            migrationBuilder.DropTable(
                name: "AttentionHour");

            migrationBuilder.DropTable(
                name: "ClinicalHistory");

            migrationBuilder.DropTable(
                name: "MedicalSchedule");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Medic");

            migrationBuilder.DropTable(
                name: "ConsultingRoom");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "MedicalSpecialty");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
