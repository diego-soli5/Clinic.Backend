using Clinic.Core.DTOs.Account;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost(nameof(Authenticate))]
        public async Task<IActionResult> Authenticate(LoginRequestDTO login)
        {
            var result = await _accountService.TryAuthenticateAsync(login);

            if (!result.Item1)
            {
                return Unauthorized(new UnauthorizedResponse(result.Item2));
            }

            var response = new OkResponse()
            {
                Data = result.Item3
            };
        
            return Ok(response);
        }
    }
}
