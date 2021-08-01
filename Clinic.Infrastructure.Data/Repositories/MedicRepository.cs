using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class MedicRepository : GenericRepository<Medic>, IMedicRepository
    {
        private const string SpMedic_GetAllPendingForUpdate = nameof(SpMedic_GetAllPendingForUpdate);

        public MedicRepository(AppDbContext context, IConfiguration configuration)
            : base(context: context, configuration: configuration)
        { }

        public IEnumerable<Medic> GetAllForList(int? medicalSpecialtyId, int? identification)
        {
            var lst = _dbEntity.Include(med => med.Employee).ThenInclude(emp => emp.AppUser)
                               .Include(med => med.Employee).ThenInclude(emp => emp.Person)
                               .Include(med => med.MedicalSpecialty)
                               .Where(med => med.Employee.AppUser.EntityStatus == Core.Enumerations.EntityStatus.Enabled)
                               .Where(med => med.IdMedicalSpecialty == (medicalSpecialtyId ?? med.IdMedicalSpecialty))
                               .Where(med => med.Employee.Person.Identification.ToString().Contains(identification.ToString()))
                               .AsEnumerable();

            return lst;
        }

        public async Task<Medic> GetByIdForEditAsync(int id)
        {
            var med = await _dbEntity.Include(med => med.Employee).ThenInclude(emp => emp.Person)
                                     .Include(med => med.Employee).ThenInclude(emp => emp.AppUser)
                                     .FirstOrDefaultAsync(med => med.Id == id);

            return med;
        }
    }
}
