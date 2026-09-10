using EPMS.Application.DTOs.DepartmentDTOs;
using EPMS.Application.DTOs.Shared;

namespace EPMS.Application.Services.DepartmentQueryService.CommandService
{
    public interface IDepartmentCommandService
    {
        public interface IDepartmentCommandService
        {
            Task<ApiResponseDto<DepartmentResponse>> CreateAsync(CreateDepartmentRequest request);

            Task<ApiResponseDto<DepartmentResponse>> UpdateAsync(Guid id, UpdateDepartmentRequest request);
        }
    }
}
