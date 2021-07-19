using Clinic.Core.CustomEntities;
using Clinic.Core.CustomExceptions;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Options;
using Clinic.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class MedicService : IMedicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public MedicService(IUnitOfWork unitOfWork,
                            IOptions<PaginationOptions> paginationOptions)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<PagedList<Medic>> GetAllAsync(MedicQueryFilter filters)
        {
            filters.PageNumber ??= _paginationOptions.DefaultPageNumber;
            filters.PageSize ??= _paginationOptions.DefaultPageSize;

            var medicList = await _unitOfWork.Medic.GetAllForListAsync(filters.MedicalSpecialty, filters.Identification);

            var pagedMedics = PagedList<Medic>.Create(medicList, filters.PageNumber.Value, filters.PageSize.Value);

            return pagedMedics;
        }

        public IEnumerable<MedicalSpecialty> GetAllMedicalSpecialties()
        {
            var listMedSpec = _unitOfWork.MedicalSpecialty.GetAll();

            return listMedSpec;
        }

        public async Task<IEnumerable<Medic>> GetAllPendingForUpdate()
        {
            var medicList = await _unitOfWork.Medic.GetAllPendingForUpdateAsync();

            return medicList;
        }

        public async Task<Employee> GetMedicPendingForUpdate(int idEmployee)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(idEmployee, includeProperties: $"{nameof(Employee.Medic)},{nameof(Employee.Person)}");

            if (emp == null)
                throw new BusisnessException("El médico no existe.");

            if (emp.EmployeeRole != Enumerations.EmployeeRole.Medic)
                throw new BusisnessException("El empleado no tiene rol de médico.");

            if (emp.Medic != null)
                throw new BusisnessException("El médico ya tiene su información actualizada.");

            return emp;

        }
    }
}
