using Clinic.Core.CustomEntities;
using Clinic.Core.CustomExceptions;
using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Options;
using Clinic.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class EmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public EmployeeService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = paginationOptions.Value;
        }

        public PagedList<Employee> GetAll(EmployeeQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber != null && filters.PageNumber > 0 ? filters.PageNumber.Value : _paginationOptions.DefaultPageNumber;
            filters.PageSize = filters.PageSize != null && filters.PageSize > 0 ? filters.PageSize.Value : _paginationOptions.DefaultPageSize;

            var employeeList = _unitOfWork.Employee.GetAll(includeProperties: $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");

            if (filters.Identification != null)
            {
                employeeList = employeeList.Where(x => x.Person.Identification.ToString().Contains(filters.Identification.Value.ToString()));
            }

            if (filters.HireDate != null)
            {
                employeeList = employeeList.Where(x => x.HireDate.ToShortDateString() == filters.HireDate?.ToShortDateString());
            }

            if (filters.EmployeeRole != null)
            {
                employeeList = employeeList.Where(x => x.EmployeeRole == filters.EmployeeRole);
            }

            if (filters.EmployeeStatus != null)
            {
                employeeList = employeeList.Where(x => x.EmployeeStatus == filters.EmployeeStatus);
            }

            var pagedEmployees = PagedList<Employee>.Create(employeeList, filters.PageNumber.Value, filters.PageSize.Value);

            return pagedEmployees;
        }

        public async Task<bool> Create(Employee employee)
        {
            var employeeList = _unitOfWork.Employee.GetAll(includeProperties: $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");

            if (employeeList.Any(x => x.AppUser.UserName == employee.AppUser.UserName))
            {
                throw new BusisnessException("El nombre de usuario ya está en uso.");
            }

            if (employeeList.Any(x => x.Person.Identification == employee.Person.Identification))
            {
                throw new BusisnessException("El número de identificación ya está en uso.");
            }

            employee.AppUser.EntityStatus = EntityStatus.Enabled;

            employee.EmployeeStatus = EmployeeStatus.Active;

            employee.HireDate = DateTime.Now;

            _unitOfWork.Employee.Create(employee);

            return await _unitOfWork.Save();
        }
    }
}
