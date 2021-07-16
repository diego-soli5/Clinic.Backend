using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Infrastructure.Data.Migrations
{
    public partial class SpMedic_GetAllPendingForUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @" CREATE PROCEDURE SpMedic_GetAllPendingForUpdate
							AS
							BEGIN
								SELECT e.Id,
									   p.Identification,
									   p.Names,
									   p.Surnames
								FROM Employee e
								INNER JOIN Person p
								ON e.IdPerson = p.Id
								FULL OUTER JOIN Medic m
								ON m.IdEmployee = e.Id
								WHERE e.EmployeeRole = 'Medic'
								AND m.IdEmployee IS NULL
							END";

			migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string sql = "DROP PROCEDURE SpMedic_GetAllPendingForUpdate";

			migrationBuilder.Sql(sql);
		}
    }
}
