using Clinic.Core.CustomEntities;
using Clinic.Core.CustomExceptions;
using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Clinic.Core.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class EmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
