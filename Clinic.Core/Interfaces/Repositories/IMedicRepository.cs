using Clinic.Core.Entities;
using System.Collections.Generic;

namespace Clinic.Core.Interfaces.Repositories
{
    public interface IMedicRepository : IGenericRepository<Medic>
    {
        IEnumerable<Medic> GetAllForList(int? medicalSpecialtyId, int? identification);
    }
}
