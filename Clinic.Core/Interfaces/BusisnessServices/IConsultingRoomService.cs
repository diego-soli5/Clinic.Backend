using Clinic.Core.Entities;
using System.Collections.Generic;

namespace Clinic.Core.Interfaces.BusisnessServices
{
    public interface IConsultingRoomService
    {
        IEnumerable<ConsultingRoom> GetAll();
    }
}
