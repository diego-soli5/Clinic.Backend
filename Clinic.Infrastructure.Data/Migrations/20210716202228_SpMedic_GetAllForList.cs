using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Infrastructure.Data.Migrations
{
    public partial class SpMedic_GetAllForList : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			string sql = @"CREATE PROCEDURE SpMedic_GetAllForList(
								@medicSpecialtyId int = null,
								@identification int = null
							)
							AS
							BEGIN
								SELECT m.Id,
									   p.Identification,
									   p.Names,
									   p.Surnames,
									   ms.Name
								FROM Medic m
								INNER JOIN Employee e
								ON m.IdEmployee = e.Id
								INNER JOIN AppUser a
								ON e.IdAppUser = a.Id
								INNER JOIN Person p
								ON e.IdPerson = p.Id
								INNER JOIN MedicalSpecialty ms
								ON m.IdMedicalSpecialty = ms.Id
								WHERE a.EntityStatus = 'Enabled' 
								AND e.EmployeeStatus = 'Active'
								AND ms.EntityStatus = 'Enabled'
								AND ((@medicSpecialtyId IS NULL) OR (ms.Id = @medicSpecialtyId))
								AND ((@identification IS NULL) OR (p.Identification LIKE CONCAT('%',@identification,'%')));
							END;";

			migrationBuilder.Sql(sql);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			string sql = "DROP PROCEDURE SpMedic_GetAllForList";

			migrationBuilder.Sql(sql);
		}
	}
}
