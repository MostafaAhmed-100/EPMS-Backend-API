using EPMS.Application.DTOs.EvaluationDTOs;
using EPMS.Application.DTOs.Shared;

namespace EPMS.Application.Services.EvaluationsService.CommandService
{
    public interface IEvaluationCommandService
    {
        Task<ApiResponseDto<Guid>> InitiateEvaluationAsync(InitiateEvaluationRequest request);

        Task<ApiResponseDto<string>> SubmitScoresAsync(Guid evaluationId, SubmitEvaluationResponsesRequest request);

        Task<ApiResponseDto<string>> CancelEvaluationAsync(Guid evaluationId);
    }
}
