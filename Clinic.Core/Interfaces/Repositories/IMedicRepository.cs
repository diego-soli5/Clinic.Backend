using Clinic.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IMedicRepository : IGenericRepository<Medic>
    {
        Task<IEnumerable<Medic>> GetAllForListAsync(int? medicalSpecialtyId, int? identification);
        Task<IEnumerable<Medic>> GetAllPendingForUpdateAsync();
    }
}
