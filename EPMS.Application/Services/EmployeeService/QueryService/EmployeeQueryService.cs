using AutoMapper;
using EPMS.Application.DTOs.EmployeeDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EPMS.Application.Services.EmployeeService.QueryService
{
    public class EmployeeQueryService : IEmployeeQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeQueryService> _logger;

        public EmployeeQueryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<EmployeeQueryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<EmployeeResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var employee = await _unitOfWork.Employees.GetByIdAsync(id);

                if (employee == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Employee {EmployeeId}.", id);
                    throw new KeyNotFoundException($"Employee with ID '{id}' does not exist.");
                }

                return new ApiResponseDto<EmployeeResponse>
                {
                    Message = "Employee retrieved successfully.",
                    Data = _mapper.Map<EmployeeResponse>(employee)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting Employee {EmployeeId}", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IReadOnlyList<EmployeeResponse>>> GetAllAsync()
        {
            try
            {
                var employees = await _unitOfWork.Employees.GetAllAsync();

                return new ApiResponseDto<IReadOnlyList<EmployeeResponse>>
                {
                    Message = "Employees retrieved successfully.",
                    Data = _mapper.Map<IReadOnlyList<EmployeeResponse>>(employees)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting All Employees");
                throw;
            }
        }

        public async Task<ApiResponseDto<IReadOnlyList<EmployeeResponse>>> GetByDepartmentIdAsync(Guid departmentId)
        {
            try
            {
                var employees = await _unitOfWork.Employees.GetByDepartmentIdAsync(departmentId);

                return new ApiResponseDto<IReadOnlyList<EmployeeResponse>>
                {
                    Message = "Department employees retrieved successfully.",
                    Data = _mapper.Map<IReadOnlyList<EmployeeResponse>>(employees)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting Employees for Department {DepartmentId}", departmentId);
                throw;
            }
        }
    }
}
