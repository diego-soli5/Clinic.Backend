using Clinic.Core.Entities;
using Clinic.Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Clinic.Core.Interfaces.InfrastructureServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Clinic.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly AuthenticationOptions _options;

        public TokenService(IOptions<AuthenticationOptions> options)
        {
            _options = options.Value;
        }

        public (string, ClaimsPrincipal) GenerateJWToken(Employee employee)
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

            //identity | principal
            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            //payload 
            var payload = new JwtPayload(
                _options.Issuer,
                _options.Audience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(60));

            //signature
            var token = new JwtSecurityToken(header, payload);
            
            return (new JwtSecurityTokenHandler().WriteToken(token),principal);
        }

        public string GenerateSMToken()
        {
            string token = Guid.NewGuid().ToString().Split('-')[1];

            return token;
        }
    }
}
