using Clinic.Core.DTOs.Account;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;
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

        [HttpPost(nameof(PasswordChangeRequest))]
        public async Task<IActionResult> PasswordChangeRequest(PasswordChangeRequestDTO request)
        {
            return (await _accountService.PasswordChangeRequest(request)) ? Ok(null) : Unauthorized(null);
        }

        [HttpPost(nameof(ChangePassword))]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO request)
        {
            return await _accountService.ChangePassword(request) 
                                        ? Ok(null) 
                                        : StatusCode(StatusCodes.Status500InternalServerError,
                                                     new { Message = "Ocurrio un error, intentalo más tarde." });
        }
    }
}
