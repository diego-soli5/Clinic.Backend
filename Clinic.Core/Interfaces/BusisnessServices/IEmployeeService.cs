using Clinic.Core.CustomEntities;
using Clinic.Core.Entities;
using Clinic.Core.QueryFilters;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.BusisnessServices
{
    public interface IEmployeeService
    {
        Task<bool> Create(Employee employee, IFormFile image);
        Task<bool> Update(Employee employee, int id, IFormFile image);
        Task<bool> Delete(int id, int appUserId, string pass);
        PagedList<Employee> GetAll(EmployeeQueryFilter filters);
        Task<Employee> GetByIdAsync(int id);
        Task<bool> Fire(int id);
        Task<bool> Activate(int id);
    }
}