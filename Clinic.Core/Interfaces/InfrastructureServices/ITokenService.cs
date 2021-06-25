using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces.InfrastructureServices
{
    public interface ITokenService
    {
        string GenerateJWToken(Employee employee);
        string GenerateSMToken();
    }
}
