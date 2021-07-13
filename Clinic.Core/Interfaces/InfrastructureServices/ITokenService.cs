using Clinic.Core.Entities;
using System.Security.Claims;

namespace Clinic.Core.Interfaces.InfrastructureServices
{
    public interface ITokenService
    {
        (string, ClaimsPrincipal) GenerateJWToken(Employee employee);
        string GenerateSMToken();
    }
}
