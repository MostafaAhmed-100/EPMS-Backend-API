using EPMS.Application.DTOs.AuthDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Application.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseDto<AuthResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("register-employee")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseDto<AuthResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeRequestDto request)
        {
            var result = await _authService.RegisterEmployeeAsync(request);
            return Ok(result);
        }
    }
}
