using Clinic.Core.Entities;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.Repositories;
using System.Collections.Generic;

namespace Clinic.Core.Services
{
    public class MedicalSpecialtyService : IMedicalSpecialtyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicalSpecialtyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MedicalSpecialty> GetAll()
        {
            return _unitOfWork.MedicalSpecialty.GetAll();
        }
    }
}
