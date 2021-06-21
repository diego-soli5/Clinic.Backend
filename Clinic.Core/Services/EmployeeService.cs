using Clinic.Core.CustomEntities;
using Clinic.Core.CustomExceptions;
using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Options;
using Clinic.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public EmployeeService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _unitOfWork.Employee.GetByIdAsync(id, includeProperties: $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");
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

            if (employee.EmployeeRole == EmployeeRole.Medic)
            {
                if (employee.Medic == null)
                {
                    throw new BusisnessException("Debe indicar los datos del perfil medico.");
                }
            }
            else if (employee.Medic != null)
            {
                employee.Medic = null;
            }

            employee.AppUser.EntityStatus = EntityStatus.Enabled;

            employee.EmployeeStatus = EmployeeStatus.Active;

            employee.HireDate = DateTime.Now;

            _unitOfWork.Employee.Create(employee);

            return await _unitOfWork.Save();
        }

        public async Task<bool> Update(Employee employee, int id)
        {
            var employeeList = _unitOfWork.Employee.GetAll(includeProperties: $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");

            var employeeFromDb = await _unitOfWork.Employee.GetByIdAsync(id);

            if (employeeFromDb == null)
            {
                throw new BusisnessException("La cuenta de empleado no existe.");
            }

            if (employeeList.Any(x => x.AppUser.UserName == employee.AppUser.UserName))
            {
                throw new BusisnessException("El nombre de usuario ya está en uso.");
            }

            if (employeeList.Any(x => x.Person.Identification == employee.Person.Identification))
            {
                throw new BusisnessException("El número de identificación ya está en uso.");
            }

            if (employee.EmployeeRole == EmployeeRole.Medic)
            {
                if (employee.Medic == null)
                {
                    throw new BusisnessException("Debe indicar los datos del perfil medico.");
                }

                employeeFromDb.Medic.IdConsultingRoom = employee.Medic.IdConsultingRoom;
                employeeFromDb.Medic.IdMedicalSpecialty = employee.Medic.IdMedicalSpecialty;
            }
            else if (employee.Medic != null)
            {
                employee.Medic = null;
            }

            employeeFromDb.Person.Address = employee.Person.Address;
            employeeFromDb.Person.Birthdate = employee.Person.Birthdate;
            employeeFromDb.Person.Email = employee.Person.Email;
            employeeFromDb.Person.Identification = employee.Person.Identification;
            employeeFromDb.Person.Names = employee.Person.Names;
            employeeFromDb.Person.Surnames = employee.Person.Surnames;
            employeeFromDb.Person.PhoneNumber = employee.Person.PhoneNumber;

            _unitOfWork.Employee.Update(employee);

            return await _unitOfWork.Save();
        }

        public async Task<bool> Enable(int id)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(id, includeProperties: $"{nameof(Employee.AppUser)}");

            if (employee == null)
            {
                throw new BusisnessException("La cuenta de empleado no existe.");
            }

            employee.AppUser.EntityStatus = EntityStatus.Enabled;

            _unitOfWork.Employee.Update(employee);

            return await _unitOfWork.Save();
        }

        public async Task<bool> Disable(int id)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(id, includeProperties: $"{nameof(Employee.AppUser)}");

            if (employee == null)
            {
                throw new BusisnessException("La cuenta de empleado no existe.");
            }

            employee.AppUser.EntityStatus = EntityStatus.Disabled;

            _unitOfWork.Employee.Update(employee);

            return await _unitOfWork.Save();
        }
    }
}
