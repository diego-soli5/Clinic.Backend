using Clinic.Core.CustomEntities;
using Clinic.Core.Entities;
using Clinic.Core.QueryFilters;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.BusisnessServices
{
    public interface IMedicService
    {
        Task<PagedList<Medic>> GetAllAsync(MedicQueryFilter filters);
    }
}
