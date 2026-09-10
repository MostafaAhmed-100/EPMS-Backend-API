using AutoMapper;
using EPMS.Application.DTOs.Shared;
using EPMS.Application.DTOs.TemplateDTOs;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EPMS.Application.Services.EvaluationTemplateService.QueryService
{
    public class EvaluationTemplateQueryService : IEvaluationTemplateQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EvaluationTemplateQueryService> _logger;

        public EvaluationTemplateQueryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<EvaluationTemplateQueryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<EvaluationTemplateResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var template = await _unitOfWork.EvaluationTemplates.GetByIdWithDetailsAsync(id);

                if (template == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Evaluation Template {TemplateId}.", id);
                    throw new KeyNotFoundException($"Evaluation Template with ID '{id}' does not exist.");
                }

                return new ApiResponseDto<EvaluationTemplateResponse>
                {
                    Message = "Evaluation template retrieved successfully.",
                    Data = _mapper.Map<EvaluationTemplateResponse>(template)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting Evaluation Template {TemplateId}", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IReadOnlyList<EvaluationTemplateResponse>>> GetActiveTemplatesAsync()
        {
            try
            {
                var templates = await _unitOfWork.EvaluationTemplates.GetActiveTemplatesAsync();

                return new ApiResponseDto<IReadOnlyList<EvaluationTemplateResponse>>
                {
                    Message = "Active evaluation templates retrieved successfully.",
                    Data = _mapper.Map<IReadOnlyList<EvaluationTemplateResponse>>(templates)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Getting Active Evaluation Templates");
                throw;
            }
        }
    }
}
