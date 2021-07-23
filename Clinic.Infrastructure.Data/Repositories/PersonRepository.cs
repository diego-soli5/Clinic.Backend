using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context, IConfiguration configuration)
            : base(context, configuration)
        { }

        public async Task<Person> GetPersonByIdEmployeeAsync(int idEmployee)
        {
            return await _dbEntity.FirstOrDefaultAsync(per => per.Employee.Id == idEmployee);
        }
    }
}
