using AutoMapper;
using EPMS.Application.DTOs.EmployeeDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Domain.Entitys;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EPMS.Application.Services.EmployeeService.CommandService
{
    public class EmployeeCommandService : IEmployeeCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeCommandService> _logger;

        public EmployeeCommandService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<EmployeeCommandService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<EmployeeResponse>> CreateAsync(CreateEmployeeRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(request.DepartmentId);
                if (department == null)
                {
                    _logger.LogWarning("Attempted to assign non-existent Department {DepartmentId} to new employee.", request.DepartmentId);
                    throw new KeyNotFoundException($"Department with ID '{request.DepartmentId}' does not exist.");
                }

                var employee = new Employee(
                    request.FirstName,
                    request.LastName,
                    request.JobTitle,
                    request.HireDate,
                    request.DepartmentId);

                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created new employee {EmployeeId} in department {DepartmentId}.", employee.Id, request.DepartmentId);
                transaction.Commit();

                return new ApiResponseDto<EmployeeResponse>
                {
                    Message = "Employee created successfully.",
                    Data = _mapper.Map<EmployeeResponse>(employee)
                };
            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                _logger.LogError(ex, "Error occurred while Creating Employee {FirstName} {LastName}", request.FirstName, request.LastName);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> ChangeDepartmentAsync(Guid employeeId, UpdateEmployeeDepartmentRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);
                if (employee == null)
                {
                    _logger.LogWarning("Attempted to change department for non-existent Employee {EmployeeId}.", employeeId);
                    throw new KeyNotFoundException($"Employee with ID '{employeeId}' does not exist.");
                }

                var department = await _unitOfWork.Departments.GetByIdAsync(request.NewDepartmentId);
                if (department == null)
                {
                    _logger.LogWarning("Attempted to transfer employee to non-existent Department {DepartmentId}.", request.NewDepartmentId);
                    throw new KeyNotFoundException($"Target Department with ID '{request.NewDepartmentId}' does not exist.");
                }

                employee.ChangeDepartment(request.NewDepartmentId);

                _unitOfWork.Employees.Update(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully transferred Employee {EmployeeId} to Department {DepartmentId}.", employeeId, request.NewDepartmentId);
                 transaction.Commit();

                return new ApiResponseDto<string>
                {
                    Message = "Employee department updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                _logger.LogError(ex, "Error occurred while Changing Department for Employee {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeactivateAsync(Guid employeeId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);
                if (employee == null)
                {
                    _logger.LogWarning("Attempted to deactivate non-existent Employee {EmployeeId}.", employeeId);
                    throw new KeyNotFoundException($"Employee with ID '{employeeId}' does not exist.");
                }

                employee.DeactivateAccount();

                _unitOfWork.Employees.Update(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deactivated Employee {EmployeeId}.", employeeId);
                transaction.Commit();

                return new ApiResponseDto<string>
                {
                    Message = "Employee account deactivated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Error occurred while Deactivating Employee {EmployeeId}", employeeId);
                throw;
            }
        }
    }
}
