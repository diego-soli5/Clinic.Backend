using Clinic.Core.CustomEntities;
using Clinic.Core.Entities;
using Clinic.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.BusisnessServices
{
    public interface IMedicService
    {
        PagedList<Medic> GetAllForList(MedicQueryFilter filters);
        IEnumerable<Employee> GetAllPendingForUpdate();
        Task<Employee> GetMedicPendingForUpdate(int idEmployee);
        Task<bool> UpdatePendingMedic(Medic entity);
        Task<Medic> GetMedicForEdit(int id);
        Task<bool> Edit(Medic entity);
    }
}
