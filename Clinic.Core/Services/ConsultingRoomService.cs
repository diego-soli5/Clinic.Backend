using Clinic.Core.Entities;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.Repositories;
using System.Collections.Generic;

namespace Clinic.Core.Services
{
    public class ConsultingRoomService : IConsultingRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsultingRoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ConsultingRoom> GetAll()
        {
            return _unitOfWork.ConsultingRoom.GetAll();
        }
    }
}
