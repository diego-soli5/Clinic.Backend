using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class MedicRepository : GenericRepository<Medic>, IMedicRepository
    {
        private const string SpMedic_GetAllForList = "SpMedic_GetAllForList";
        private const string SpMedic_GetAllPendingForUpdate = "SpMedic_GetAllPendingForUpdate";

        public MedicRepository(AppDbContext context, IConfiguration configuration)
            : base(context: context, configuration: configuration)
        { }

        public async Task<IEnumerable<Medic>> GetAllForListAsync(int? medicalSpecialtyId, int? identification)
        {
            List<Medic> medicList = new List<Medic>();

            var parameters = new[]
            {
                new SqlParameter("@medicSpecialtyId", medicalSpecialtyId),
                new SqlParameter("@identification", identification)
            };

            var table = await ExecuteQuery(SpMedic_GetAllForList, parameters);

            foreach (DataRow row in table?.Rows)
            {
                medicList.Add(new Medic
                {
                    Id = (int)row["Id"],
                    Employee = new Employee
                    {
                        Person = new Person
                        {
                            Identification = (int)row["Identification"],
                            Names = row["Names"].ToString(),
                            Surnames = row["Surnames"].ToString()
                        }
                    },
                    MedicalSpecialty = new MedicalSpecialty
                    {
                        Name = row["Name"].ToString()
                    }
                });
            }

            return medicList;
        }

        public async Task<IEnumerable<Medic>> GetAllPendingForUpdateAsync()
        {
            List<Medic> medicList = new List<Medic>();

            var table = await ExecuteQuery(SpMedic_GetAllForList);

            foreach (DataRow row in table?.Rows)
            {
                medicList.Add(new Medic
                {
                    Employee = new Employee
                    {
                        Id = (int)row["Id"],
                        Person = new Person
                        {
                            Identification = (int)row["Identification"],
                            Names = (string)row["Names"],
                            Surnames = (string)row["Surnames"]
                        }
                    },
                });
            }

            return medicList;
        }
    }
}
