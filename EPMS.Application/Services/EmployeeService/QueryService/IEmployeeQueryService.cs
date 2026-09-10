using EPMS.Application.DTOs.EmployeeDTOs;
using EPMS.Application.DTOs.Shared;

namespace EPMS.Application.Services.EmployeeService.QueryService
{
    public interface IEmployeeQueryService
    {
        Task<ApiResponseDto<EmployeeResponse>> GetByIdAsync(Guid id);

         Task<ApiResponseDto<IReadOnlyList<EmployeeResponse>>> GetAllAsync();

        Task<ApiResponseDto<IReadOnlyList<EmployeeResponse>>> GetByDepartmentIdAsync(Guid departmentId);
    }
}
