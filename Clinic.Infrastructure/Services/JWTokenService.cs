using Clinic.Core.Entities;
using Clinic.Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Clinic.Core.Interfaces.InfrastructureServices;

namespace Clinic.Infrastructure.Services
{
    public class JWTokenService : IJWTokenService
    {
        private readonly AuthenticationOptions _options;
        
        public JWTokenService(IOptions<AuthenticationOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(Employee employee)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var sigingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(sigingCredentials);

            //claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{employee.Person.Names} {employee.Person.Surnames}"),
                new Claim(ClaimTypes.Email,employee.Person.Email),
                new Claim(ClaimTypes.MobilePhone, employee.Person.PhoneNumber.ToString()),
                new Claim(ClaimTypes.Role,employee.AppUser.Role.ToString()),
                new Claim(ClaimTypes.Role,employee.EmployeeRole.ToString())
            };

            //payload 
            var payload = new JwtPayload(
                _options.Issuer,
                _options.Audience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2));

            //signature
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
