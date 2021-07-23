using Clinic.Core.CustomExceptions;
using Clinic.Core.DTOs.Account;
using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.EmailServices;
using Clinic.Core.Interfaces.ExternalServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        private readonly IBusisnessMailService _mailService;
        private readonly IAzureBlobFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        private const string _INVALIDCREDENTIALS = "Credenciales de inicio invalidas.";
        private const string _DISABLEDACCOUNT = "La cuenta está deshabilidata.";

        public AccountService(ITokenService service,
                              IUnitOfWork unitOfWork,
                              IPasswordService passwordService,
                              IBusisnessMailService mailService,
                              IAzureBlobFileService fileService)
        {
            _tokenService = service;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _mailService = mailService;
            _fileService = fileService;
        }

        public async Task<Person> GetCurrentUser(int idEmployee)
        {
            var userInfo = await _unitOfWork.Person.GetPersonByIdEmployeeAsync(idEmployee);

            if (userInfo == null)
                throw new NotFoundException("El empleado solicitado no existe.", idEmployee);

            return userInfo;
        }

        public async Task<(bool, string, LoginResultDTO, ClaimsPrincipal)> TryAuthenticateAsync(LoginRequestDTO login)
        {
            int? identification = null;
            Employee oEmployee;

            try
            {
                identification = Convert.ToInt32(login.EmailOrIdentification);
            }
            catch (Exception) { }

            if (identification.HasValue)
            {
                oEmployee = await _unitOfWork.Employee.GetByIdentification(identification.Value);
            }
            else
            {
                oEmployee = await _unitOfWork.Employee.GetByEmail(login.EmailOrIdentification);
            }

            if (oEmployee == null)
                return (false, _INVALIDCREDENTIALS, null, null);

            if (oEmployee.AppUser.EntityStatus == Enumerations.EntityStatus.Disabled)
                return (false, _DISABLEDACCOUNT, null, null);

            if (!_passwordService.Check(oEmployee.AppUser.Password, login.Password))
                return (false, _INVALIDCREDENTIALS, null, null);

            var tokenGenerationResult = _tokenService.GenerateJWToken(oEmployee);

            string token = tokenGenerationResult.Item1;

            var loginResult = new LoginResultDTO
            {
                Id = oEmployee.Id,
                FullName = $"{oEmployee.Person.Names} {oEmployee.Person.Surnames}",
                Email = oEmployee.Person.Email,
                PhoneNumber = oEmployee.Person.PhoneNumber,
                ImageName = oEmployee.Person.ImageName,
                AppUserRole = oEmployee.AppUser.Role,
                EmployeeRole = oEmployee.EmployeeRole,
                Token = token
            };

            return (true, null, loginResult, tokenGenerationResult.Item2);
        }

        public async Task<bool> PasswordChangeRequest(PasswordChangeRequestDTO request)
        {
            var oEmployee = await _unitOfWork.Employee.GetByIdAsync(request.Id, $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");

            if (oEmployee == null)
            {
                throw new BusisnessException("La cuenta de empleado no existe.");
            }

            if (oEmployee.AppUser.EntityStatus == Enumerations.EntityStatus.Disabled)
            {
                throw new BusisnessException("La cuenta de empleado está deshabilitada.");
            }

            if (!_passwordService.Check(oEmployee.AppUser.Password, request.Password))
            {
                return false;
            }

            string token = _tokenService.GenerateSMToken();
            oEmployee.AppUser.SMToken = token;

            _unitOfWork.AppUser.Update(oEmployee.AppUser);
            await _unitOfWork.Save();

            string subject = "Solicitud de cambio de contraseña.";
            string body = $"Hola {oEmployee.Person.Names}!\nHemos recibido una solicitud de cambio de contraseña, tu token es el siguiente:\n{token}.";

            _mailService.SendMail(subject, body, new List<string> { oEmployee.Person.Email });

            return true;
        }

        public async Task<bool> ChangePassword(ChangePasswordDTO request)
        {
            var appUser = await _unitOfWork.AppUser.GetByIdAsync(request.Id);

            if (appUser == null)
            {
                throw new BusisnessException("La cuenta de empleado no existe.");
            }

            if (appUser.EntityStatus == Enumerations.EntityStatus.Disabled)
            {
                throw new BusisnessException("La cuenta de empleado está deshabilitada.");
            }

            if (appUser.SMToken == null)
            {
                throw new BusisnessException("Primero se debe realizar la solicitud del token.");
            }

            if (appUser.SMToken != request.Token)
            {
                throw new BusisnessException("El token no coincide.");
            }

            string encPass = _passwordService.Hash(request.NewPassword);

            appUser.Password = encPass;
            appUser.SMToken = null;

            _unitOfWork.AppUser.Update(appUser);

            return await _unitOfWork.Save();
        }

        public async Task<bool> ChangeImage(int id, IFormFile image)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(id, $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");

            if (emp == null)
                throw new BusisnessException("El empleado no existe.");

            if (emp.AppUser.EntityStatus == EntityStatus.Disabled)
                throw new BusisnessException("El empleado está desactivado, debe activarlo para poder consultarlo.");

            await _fileService.DeleteBlobAsync(emp.Person.ImageName);

            string newImgName = await _fileService.CreateBlobAsync(image);

            emp.Person.ImageName = newImgName;

            _unitOfWork.Person.Update(emp.Person);

            return await _unitOfWork.Save();
        }
    }
}


