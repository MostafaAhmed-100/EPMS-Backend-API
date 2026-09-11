using EPMS.Application.DTOs.DepartmentDTOs;
using EPMS.Application.DTOs.Shared;

namespace EPMS.Application.Services.DepartmentQueryService.QueryService
{
    public interface IDepartmentQueryService
    {
        Task<ApiResponseDto<DepartmentResponse>> GetByIdAsync(Guid id);
        Task<ApiResponseDto<IReadOnlyList<DepartmentResponse>>> GetAllAsync();
    }
}