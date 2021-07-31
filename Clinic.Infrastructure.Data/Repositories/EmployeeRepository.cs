using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context, IConfiguration configuration)
            : base(context, configuration)
        {
            
        }

        public async Task<Employee> GetByEmail(string email)
        {
            return await _dbEntity.Include(e => e.Person).Include(e => e.AppUser).FirstOrDefaultAsync(e => e.Person.Email.ToLower() == email.ToLower());
        }

        public async Task<Employee> GetByIdentification(int identification)
        {
            return await _dbEntity.Include(e => e.Person).Include(e => e.AppUser).FirstOrDefaultAsync(e => e.Person.Identification == identification);
        }

        public IEnumerable<Employee> GetAllMedicsPendingForUpdate()
        {
            var medicsPendingList = _dbEntity.Include(emp => emp.Person)
                .Where(emp => emp.EmployeeRole == EmployeeRole.Medic)
                .Where(emp => emp.Medic == null)
                .AsEnumerable();

            return medicsPendingList;
        }
    }
}
