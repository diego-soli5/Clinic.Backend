using Clinic.Core.DTOs.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clinic.Core.Interfaces.BusisnessServices
{
    public interface IAccountService
    {
        Task<(bool, string, LoginResultDTO, ClaimsPrincipal)> TryAuthenticateAsync(LoginRequestDTO login);
        Task<bool> PasswordChangeRequest(PasswordChangeRequestDTO request);
        Task<bool> ChangePassword(ChangePasswordDTO request);
    }
}
