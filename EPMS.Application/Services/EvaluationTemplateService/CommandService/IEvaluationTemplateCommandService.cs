using EPMS.Application.DTOs.Shared;
using EPMS.Application.DTOs.TemplateDTOs;

namespace EPMS.Application.Services.EvaluationTemplateService.CommandService
{
    public interface IEvaluationTemplateCommandService
    {
        Task<ApiResponseDto<Guid>> CreateAsync(CreateTemplateRequest request);

        Task<ApiResponseDto<string>> DeactivateAsync(Guid id);
    }
}
