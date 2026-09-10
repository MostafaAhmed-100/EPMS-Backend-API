using AutoMapper;
using EPMS.Application.DTOs.DepartmentDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Domain.Entitys;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EPMS.Application.Services.DepartmentQueryService.CommandService
{
    public class DepartmentCommandService : IDepartmentCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentCommandService> _logger;

        public DepartmentCommandService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DepartmentCommandService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<DepartmentResponse>> CreateAsync(CreateDepartmentRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = new Department(request.Name, request.Description);

                await _unitOfWork.Departments.AddAsync(department);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new department {DepartmentId}.", department.Id);
                 transaction.Commit();

                return new ApiResponseDto<DepartmentResponse>
                {
                    Message = "Department created successfully.",
                    Data = _mapper.Map<DepartmentResponse>(department)
                };
            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                _logger.LogError(ex, "Error occurred while Creating Department {DepartmentName}", request.Name);
                throw;
            }
        }

        public async Task<ApiResponseDto<DepartmentResponse>> UpdateAsync(Guid id, UpdateDepartmentRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(id);

                if (department == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Department {DepartmentId}.", id);
                    throw new KeyNotFoundException($"Department with ID '{id}' does not exist.");
                }

                department.UpdateDetails(request.Name, request.Description);

                _unitOfWork.Departments.Update(department);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Department {DepartmentId}.", id);
                 transaction.Commit();

                return new ApiResponseDto<DepartmentResponse>
                {
                    Message = "Department updated successfully.",
                    Data = _mapper.Map<DepartmentResponse>(department)
                };
            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                _logger.LogError(ex, "Error occurred while Updating Department {DepartmentId}", id);
                throw;
            }
        }
    }
}
