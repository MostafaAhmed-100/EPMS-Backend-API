using EPMS.Application.DTOs.AuthDTOs;
using EPMS.Application.DTOs.Shared;

namespace EPMS.Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginRequestDto request);
        Task<ApiResponseDto<AuthResponseDto>> RegisterEmployeeAsync(RegisterEmployeeRequestDto request);
    }
}
