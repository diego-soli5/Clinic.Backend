using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context)
            : base(context)
        {
            
        }

        public async Task<Employee> GetByEmail(string email)
        {
            return await _dbEntity.Include(e => e.Person).FirstOrDefaultAsync(e => e.Person.Email.ToLower() == email.ToLower());
        }

        public async Task<Employee> GetByIdentification(int identification)
        {
            return await _dbEntity.Include(e => e.Person).FirstOrDefaultAsync(e => e.Person.Identification == identification);
        }
    }
}
