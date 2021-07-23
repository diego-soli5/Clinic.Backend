using AutoMapper;
using Clinic.Core.DTOs.Account;
using Clinic.Core.DTOs.Person;
using Clinic.Core.Interfaces.BusisnessServices;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetCurrentUser) + "/{id}")]
        public async Task<IActionResult> GetCurrentUser(int id)
        {
            var userInfo = await _accountService.GetCurrentUser(id);

            var response = new OkResponse
            {
                Data = _mapper.Map<PersonDTO>(userInfo)
            };

            return Ok(response);
        }

        [HttpPost(nameof(Authenticate))]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginRequestDTO login)
        {
            var result = await _accountService.TryAuthenticateAsync(login);

            if (!result.Item1)
            {
                return BadRequest(new BadRequestResponse(result.Item2));
            }

            var response = new OkResponse()
            {
                Data = result.Item3
            };

            return Ok(response);
        }

        [HttpPost("ChangeImage/{id}")]
        public async Task<IActionResult> ChangeImage(int id, [FromForm] IFormFile image)
        {
            if (image == null)
                return BadRequest(new BadRequestResponse("Se esperaba una imagen válida."));

            if (!ValidImageTypes().Any(x => x == image.ContentType))
                return BadRequest(new BadRequestResponse("Sólo se aceptan imagenes de tipo png, jpg o jpeg."));

            await _accountService.ChangeImage(id, image);

            return Ok(new OkResponse());
        }

        [HttpPost(nameof(PasswordChangeRequest))]
        public async Task<IActionResult> PasswordChangeRequest(PasswordChangeRequestDTO request)
        {
            return (await _accountService.PasswordChangeRequest(request)) ? NoContent() : Unauthorized(null);
        }

        [HttpPost(nameof(ChangePassword))]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO request)
        {
            return await _accountService.ChangePassword(request)
                                        ? NoContent()
                                        : StatusCode(StatusCodes.Status500InternalServerError,
                                                     new { Message = "Ocurrio un error, intentalo más tarde." });
        }

        #region UTILITY METHODS
        private string[] ValidImageTypes()
        {
            return new[]
            {
                "image/png",
                "image/jpg",
                "image/jpeg"
            };
        }
        #endregion
    }
}
