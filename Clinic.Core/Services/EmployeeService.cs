using Clinic.Core.CustomEntities;
using Clinic.Core.CustomExceptions;
using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Core.Interfaces.EmailServices;
using Clinic.Core.Interfaces.ExternalServices;
using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.Interfaces.Repositories;
using Clinic.Core.Options;
using Clinic.Core.QueryFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureBlobFileService _blobFileService;
        private readonly IBusisnessMailService _mailService;
        private readonly IPasswordService _passwordService;
        private readonly ImageOptions _imageOptions;
        private readonly PaginationOptions _paginationOptions;

        public EmployeeService(IUnitOfWork unitOfWork,
                               IOptions<PaginationOptions> paginationOptions,
                               IAzureBlobFileService blobFileService,
                               IBusisnessMailService mailService,
                               IPasswordService passwordService,
                               ImageOptions imageOptions)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = paginationOptions.Value;
            _blobFileService = blobFileService;
            _mailService = mailService;
            _passwordService = passwordService;
            _imageOptions = imageOptions;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(id, includeProperties: $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");

            if (emp == null)
                throw new NotFoundException("El empleado no existe.", id);

            if (emp.AppUser.EntityStatus == EntityStatus.Disabled)
                throw new BusisnessException("El empleado está desactivado, debe activarlo para poder consultarlo.");

            return emp;
        }

        public PagedList<Employee> GetAll(EmployeeQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber != null && filters.PageNumber > 0 ? filters.PageNumber.Value : _paginationOptions.DefaultPageNumber;
            filters.PageSize = filters.PageSize != null && filters.PageSize > 0 ? filters.PageSize.Value : _paginationOptions.DefaultPageSize;

            var employeeList = _unitOfWork.Employee.GetAll(includeProperties: $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");

            if (filters.Identification != null)
            {
                employeeList = employeeList.Where(x => x.Person.Identification.ToString().Contains(filters.Identification.Value.ToString()));
            }

            if (filters.HireDate != null)
            {
                employeeList = employeeList.Where(x => x.HireDate.ToShortDateString() == filters.HireDate?.ToShortDateString());
            }

            if (filters.EmployeeRole != null)
            {
                employeeList = employeeList.Where(x => x.EmployeeRole == filters.EmployeeRole);
            }

            if (filters.EmployeeStatus != null)
            {
                employeeList = employeeList.Where(x => x.EmployeeStatus == filters.EmployeeStatus);
            }

            employeeList = employeeList.Where(x => x.AppUser.EntityStatus == EntityStatus.Enabled);

            var pagedEmployees = PagedList<Employee>.Create(employeeList, filters.PageNumber.Value, filters.PageSize.Value);

            return pagedEmployees;
        }

        public async Task<bool> Create(Employee employee, IFormFile image)
        {
            var employeeList = _unitOfWork.Employee.GetAll(includeProperties: $"{nameof(Employee.AppUser)},{nameof(Employee.Person)}");

            if (employeeList.Any(x => x.AppUser.UserName == employee.AppUser.UserName))
            {
                throw new BusisnessException("El nombre de usuario ya está en uso.");
            }

            if (employeeList.Any(x => x.Person.Identification == employee.Person.Identification))
            {
                throw new BusisnessException("El número de identificación ya está en uso.");
            }

            if (employeeList.Any(x => x.Person.PhoneNumber == employee.Person.PhoneNumber))
            {
                throw new BusisnessException("El número de teléfono ya está en uso.");
            }

            if (employeeList.Any(x => x.Person.Email == employee.Person.Email))
            {
                throw new BusisnessException("La dirección de correo electrónico ya está en uso.");
            }

            string encryptedPassword = _passwordService.Hash(employee.AppUser.Password);
            employee.AppUser.EntityStatus = EntityStatus.Enabled;
            employee.EmployeeStatus = EmployeeStatus.Active;
            employee.HireDate = DateTime.Now;
            employee.AppUser.Password = encryptedPassword;

            if (image != null)
                employee.Person.ImageName = await _blobFileService.CreateBlobAsync(image);
            else
                employee.Person.ImageName = _imageOptions.DefaultEmployeeImage;

            _unitOfWork.Employee.Create(employee);

            return await _unitOfWork.Save();
        }

        public async Task<bool> Update(Employee employee, int id, IFormFile image)
        {
            var employeeList = _unitOfWork.Employee.GetAll(includeProperties: $"{nameof(Employee.Person)}");

            var employeeFromDb = await _unitOfWork.Employee.GetByIdAsync(id, $"{nameof(Employee.AppUser)}");

            if (employeeFromDb == null)
                throw new BusisnessException("El empleado no existe.");

            if (employeeFromDb.AppUser.EntityStatus == EntityStatus.Disabled)
                throw new BusisnessException("El empleado está desactivado, debe activarlo para poder consultarlo.");

            int empValidationId = employeeList.Where(x => x.Person.Email.Trim().ToLower() == employee.Person.Email.Trim().ToLower()).Select(x => x.Id).FirstOrDefault();

            if (empValidationId != 0)
            {
                if (empValidationId != id)
                    throw new BusisnessException("La dirección de correo electrónico ya está en uso.");
            }

            empValidationId = employeeList.Where(x => x.Person.Identification == employee.Person.Identification).Select(x => x.Id).FirstOrDefault();

            if (empValidationId != 0)
            {
                if (empValidationId != id)
                    throw new BusisnessException("El número de identificación ya está en uso.");
            }

            empValidationId = employeeList.Where(x => x.Person.PhoneNumber == employee.Person.PhoneNumber).Select(x => x.Id).FirstOrDefault();

            if (empValidationId != 0)
            {
                if (empValidationId != id)
                    throw new BusisnessException("El número de telefono ya está en uso.");
            }

            bool ok = false;

            if (image != null)
            {
                if (employeeFromDb.Person.ImageName?.Length > 0)
                {
                    await _blobFileService.DeleteBlobAsync(employeeFromDb.Person.ImageName);
                }

                employeeFromDb.Person.ImageName = await _blobFileService.CreateBlobAsync(image);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                employeeFromDb.EmployeeRole = employee.EmployeeRole;
                employeeFromDb.Person.Identification = employee.Person.Identification;
                employeeFromDb.Person.Address = employee.Person.Address;
                employeeFromDb.Person.Birthdate = employee.Person.Birthdate;
                employeeFromDb.Person.Email = employee.Person.Email;
                employeeFromDb.Person.Names = employee.Person.Names;
                employeeFromDb.Person.Surnames = employee.Person.Surnames;
                employeeFromDb.Person.PhoneNumber = employee.Person.PhoneNumber;

                _unitOfWork.Person.Update(employeeFromDb.Person);

                ok = await _unitOfWork.Save();

                _unitOfWork.Employee.Update(employeeFromDb);

                await _unitOfWork.Save();

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackAsync();

                throw ex;
            }

            string subject = "Modificación de Perfil";
            string body = $"Hola {employeeFromDb.Person.Names}, se ha actualizado la información de tu perfil el día {DateTime.Now.ToShortDateString()} a las {DateTime.Now.ToShortTimeString()}.";

            _mailService.SendMail(subject, body, new List<string>() { employeeFromDb.Person.Email });

            return ok;
        }

        public async Task<bool> Delete(int id, int appUserId, string pass)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(id, includeProperties: $"{nameof(Employee.AppUser)}");

            if (employee == null)
            {
                throw new BusisnessException("El empleado no existe.");
            }

            string passwordFromDb = employee.AppUser.Password;

            if (!_passwordService.Check(passwordFromDb, pass))
            {
                throw new BusisnessException("La contraseña es incorrecta.");
            }

            employee.AppUser.EntityStatus = EntityStatus.Disabled;

            _unitOfWork.AppUser.Update(employee.AppUser);

            return await _unitOfWork.Save();
        }

        #region DESECHADO
        //Codigo comentado por posibilidad de reintegrar la funcionalidad
        /*
        public async Task<bool> Fire(int id)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(id);

            if (employee == null)
            {
                throw new BusisnessException("El empleado no existe.");
            }

            employee.EmployeeStatus = EmployeeStatus.Fired;

            _unitOfWork.Employee.Update(employee);

            return await _unitOfWork.Save();
        }

        public async Task<bool> Activate(int id)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(id);

            if (employee == null)
            {
                throw new BusisnessException("El empleado no existe.");
            }

            employee.EmployeeStatus = EmployeeStatus.Active;

            _unitOfWork.Employee.Update(employee);

            return await _unitOfWork.Save();
        }
        */
        #endregion
    }
}
