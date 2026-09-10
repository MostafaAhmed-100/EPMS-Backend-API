using AutoMapper;
using EPMS.Application.DTOs.EvaluationDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPMS.Application.Services.EvaluationsService.QueryService
{
    public class EvaluationQueryService : IEvaluationQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EvaluationQueryService> _logger;

        public EvaluationQueryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<EvaluationQueryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<DetailedEvaluationResponse>> GetDetailedByIdAsync(Guid evaluationId)
        {
            try
            {
                var evaluation = await _unitOfWork.Evaluations.GetByIdWithResponsesAsync(evaluationId);

                if (evaluation == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Evaluation {EvaluationId}.", evaluationId);
                    throw new KeyNotFoundException($"Evaluation with ID '{evaluationId}' does not exist.");
                }

                return new ApiResponseDto<DetailedEvaluationResponse>
                {
                    Message = "Evaluation details retrieved successfully.",
                    Data = _mapper.Map<DetailedEvaluationResponse>(evaluation)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting Detailed Evaluation {EvaluationId}", evaluationId);
                throw;
            }
        }

        public async Task<ApiResponseDto<IReadOnlyList<EvaluationSummaryResponse>>> GetByEmployeeIdAsync(Guid employeeId)
        {
            try
            {
                var evaluations = await _unitOfWork.Evaluations.GetByEmployeeIdAsync(employeeId);

                return new ApiResponseDto<IReadOnlyList<EvaluationSummaryResponse>>
                {
                    Message = "Employee evaluations retrieved successfully.",
                    Data = _mapper.Map<IReadOnlyList<EvaluationSummaryResponse>>(evaluations)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting Evaluations for Employee {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<ApiResponseDto<IReadOnlyList<EvaluationSummaryResponse>>> GetByEvaluatorIdAsync(Guid evaluatorId)
        {
            try
            {
                var evaluations = await _unitOfWork.Evaluations.GetByEvaluatorIdAsync(evaluatorId);

                return new ApiResponseDto<IReadOnlyList<EvaluationSummaryResponse>>
                {
                    Message = "Evaluator evaluations retrieved successfully.",
                    Data = _mapper.Map<IReadOnlyList<EvaluationSummaryResponse>>(evaluations)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting Evaluations for Evaluator {EvaluatorId}", evaluatorId);
                throw;
            }
        }
    }
}
