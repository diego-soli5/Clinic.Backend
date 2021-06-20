using Clinic.Core.CustomEntities;
using Clinic.Core.Entities;
using Clinic.Core.QueryFilters;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.BusisnessServices
{
    public interface IEmployeeService
    {
        Task<bool> Create(Employee employee);
        Task<bool> DisableOrEnable(int id);
        PagedList<Employee> GetAll(EmployeeQueryFilter filters);
        Task<Employee> GetByIdAsync(int id);
    }
}