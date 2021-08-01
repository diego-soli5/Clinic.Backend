using Clinic.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IMedicRepository : IGenericRepository<Medic>
    {
        IEnumerable<Medic> GetAllForList(int? medicalSpecialtyId, int? identification);
        Task<Medic> GetByIdForEditAsync(int id);
    }
}
