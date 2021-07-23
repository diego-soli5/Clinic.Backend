using Clinic.Core.Entities;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<Person> GetPersonByIdEmployeeAsync(int idEmployee);
    }
}
