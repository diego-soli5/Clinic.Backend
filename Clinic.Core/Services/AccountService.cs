using Clinic.Core.DTOs.Account;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IJWTokenService _tokenService;
        private readonly IPasswordService _passwordService;
        private readonly IUnitOfWork _unitOfWork;
        private const string _INVALIDCREDENTIALS = "Credenciales de inicio invalidas.";
        private const string _DISABLEDACCOUNT = "La cuenta está deshabilidata.";

        public AccountService(IJWTokenService service, IUnitOfWork unitOfWork, IPasswordService passwordService)
        {
            _tokenService = service;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }

        public async Task<(bool, string, LoginResultDTO)> TryAuthenticateAsync(LoginRequestDTO login)
        {
            int? identification = null;
            Employee oEmployee = null;

            try
            {
                identification = Convert.ToInt32(login.EmailOrIdentification);
            }
            catch (Exception) { }

            if (login.EmailOrIdentification.GetType() == typeof(int))
            {
                oEmployee = await _unitOfWork.Employee.GetByIdentification(identification.Value);
            }
            else
            {
                oEmployee = await _unitOfWork.Employee.GetByEmail(login.EmailOrIdentification);
            }

            if (oEmployee == null)
                return (false, _INVALIDCREDENTIALS, null);

            if (oEmployee.AppUser.EntityStatus == Enumerations.EntityStatus.Disabled)
                return (false, _DISABLEDACCOUNT, null);

            if (!_passwordService.Check(oEmployee.AppUser.Password, login.Password))
                return (false, _INVALIDCREDENTIALS, null);

            string token = _tokenService.GenerateToken(oEmployee);

            var loginResult = new LoginResultDTO
            {
                Id = oEmployee.Id,
                FullName = $"{oEmployee.Person.Names} {oEmployee.Person.Surnames}",
                Email = oEmployee.Person.Email,
                PhoneNumber = oEmployee.Person.PhoneNumber,
                AppUserRole = oEmployee.AppUser.Role,
                EmployeeRole = oEmployee.EmployeeRole,
                Token = token
            };

            return (true, null, loginResult);
        }
    }
}


