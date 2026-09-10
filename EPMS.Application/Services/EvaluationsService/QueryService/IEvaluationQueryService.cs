using EPMS.Application.DTOs.EvaluationDTOs;
using EPMS.Application.DTOs.Shared;

namespace EPMS.Application.Services.EvaluationsService.QueryService
{
    public interface IEvaluationQueryService
    {
        Task<ApiResponseDto<DetailedEvaluationResponse>> GetDetailedByIdAsync(Guid evaluationId);

        Task<ApiResponseDto<IReadOnlyList<EvaluationSummaryResponse>>> GetByEmployeeIdAsync(Guid employeeId);

        Task<ApiResponseDto<IReadOnlyList<EvaluationSummaryResponse>>> GetByEvaluatorIdAsync(Guid evaluatorId);
    }
}
