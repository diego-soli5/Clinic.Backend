using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces.InfrastructureServices
{
    public interface IJWTokenService
    {
        string GenerateToken(Employee employee);
    }
}
