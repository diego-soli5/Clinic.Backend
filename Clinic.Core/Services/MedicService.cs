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

        public PagedList<Medic> GetAllForList(MedicQueryFilter filters)
        {
            filters.PageNumber ??= _paginationOptions.DefaultPageNumber;
            filters.PageSize ??= _paginationOptions.DefaultPageSize;

            var medicList = _unitOfWork.Medic.GetAllForList(filters.MedicalSpecialty, filters.Identification);

            var pagedMedics = PagedList<Medic>.Create(medicList, filters.PageNumber.Value, filters.PageSize.Value);

            return pagedMedics;
        }

        public async Task<IEnumerable<Medic>> GetAllPendingForUpdate()
        {
            var medicList = await _unitOfWork.Medic.GetAllPendingForUpdateAsync();

            return medicList;
        }

        public async Task<Employee> GetMedicPendingForUpdate(int idEmployee)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(idEmployee, includeProperties: $"{nameof(Employee.Medic)},{nameof(Employee.Person)},{nameof(Employee.AppUser)}");

            if (emp == null)
                throw new NotFoundException("El médico no existe.", idEmployee);

            if (emp.AppUser.EntityStatus == Enumerations.EntityStatus.Disabled)
                throw new BusisnessException("El empleado está desactivado, debe activarlo para poder consultarlo.");

            if (emp.EmployeeRole != Enumerations.EmployeeRole.Medic)
                throw new BusisnessException("El empleado no tiene rol de médico.");

            if (emp.Medic != null)
                throw new BusisnessException("El médico ya tiene su información actualizada.");

            return emp;
        }

        public async Task<bool> UpdatePendingMedic(Medic entity)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(entity.IdEmployee, includeProperties: $"{nameof(Employee.Medic)},{nameof(Employee.AppUser)}");

            if (emp == null)
                throw new BusisnessException("El médico no existe.");
            if (emp.AppUser.EntityStatus == Enumerations.EntityStatus.Disabled)
                throw new BusisnessException("El empleado está desactivado, debe activarlo para poder consultarlo.");
            if (emp.EmployeeRole != Enumerations.EmployeeRole.Medic)
                throw new BusisnessException("El empleado no tiene rol de médico.");
            if (emp.Medic != null)
                throw new BusisnessException("El médico ya tiene su información actualizada.");

            var medicalSpecialty = await _unitOfWork.MedicalSpecialty.GetByIdAsync(entity.IdMedicalSpecialty);

            if (medicalSpecialty == null)
                throw new BusisnessException("La especialidad médica no existe.");
            if (medicalSpecialty.EntityStatus == Enumerations.EntityStatus.Disabled)
                throw new BusisnessException("La especialidad médica está desactivada, debe activarla para poder consultarla.");

            var consultingRoom = await _unitOfWork.ConsultingRoom.GetByIdAsync(entity.IdConsultingRoom);

            if (consultingRoom == null)
                throw new BusisnessException("El consultorio no existe.");
            if (consultingRoom.EntityStatus == Enumerations.EntityStatus.Disabled)
                throw new BusisnessException("El consultorio está desactivado, debe activarlo para poder consultarlo.");

            _unitOfWork.Medic.Create(entity);

            return await _unitOfWork.Save();
        }
    }
}
