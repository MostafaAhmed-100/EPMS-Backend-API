using AutoMapper;
using EPMS.Application.DTOs.DepartmentDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EPMS.Application.Services.DepartmentQueryService.QueryService
{
    public class DepartmentQueryService : IDepartmentQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentQueryService> _logger;

        public DepartmentQueryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DepartmentQueryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<DepartmentResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(id);

                if (department == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Department {DepartmentId}.", id);
                    throw new KeyNotFoundException($"Department with ID '{id}' does not exist.");
                }

                return new ApiResponseDto<DepartmentResponse>
                {
                    Message = "Department retrieved successfully.",
                    Data = _mapper.Map<DepartmentResponse>(department)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting Department {DepartmentId}", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IReadOnlyList<DepartmentResponse>>> GetAllAsync()
        {
            try
            {
                var departments = await _unitOfWork.Departments.GetAllAsync();

                return new ApiResponseDto<IReadOnlyList<DepartmentResponse>>
                {
                    Message = "Departments retrieved successfully.",
                    Data = _mapper.Map<IReadOnlyList<DepartmentResponse>>(departments)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting All Departments");
                throw;
            }
        }
    }
}
