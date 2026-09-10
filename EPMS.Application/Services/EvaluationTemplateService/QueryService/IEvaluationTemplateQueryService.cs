using EPMS.Application.DTOs.Shared;
using EPMS.Application.DTOs.TemplateDTOs;

namespace EPMS.Application.Services.EvaluationTemplateService.QueryService
{
    public interface IEvaluationTemplateQueryService
    {
        Task<ApiResponseDto<EvaluationTemplateResponse>> GetByIdAsync(Guid id);

        Task<ApiResponseDto<IReadOnlyList<EvaluationTemplateResponse>>> GetActiveTemplatesAsync();
    }
}
