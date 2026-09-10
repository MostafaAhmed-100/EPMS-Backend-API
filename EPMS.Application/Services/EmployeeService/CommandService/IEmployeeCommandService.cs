using EPMS.Application.DTOs.EmployeeDTOs;
using EPMS.Application.DTOs.Shared;

namespace EPMS.Application.Services.EmployeeService.CommandService
{
    public interface IEmployeeCommandService
    {
        Task<ApiResponseDto<EmployeeResponse>> CreateAsync(CreateEmployeeRequest request);

        Task<ApiResponseDto<string>> ChangeDepartmentAsync(Guid employeeId, UpdateEmployeeDepartmentRequest request);

        Task<ApiResponseDto<string>> DeactivateAsync(Guid employeeId);
    }
}
