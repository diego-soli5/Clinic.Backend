using Clinic.Core.Entities;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetByEmail(string email);
        Task<Employee> GetByIdentification(int identification);
    }
}
