using AutoMapper;
using EPMS.Application.DTOs.Shared;
using EPMS.Application.DTOs.TemplateDTOs;
using EPMS.Domain.Entitys;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPMS.Application.Services.EvaluationTemplateService.CommandService
{
    public class EvaluationTemplateCommandService : IEvaluationTemplateCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EvaluationTemplateCommandService> _logger;

        public EvaluationTemplateCommandService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<EvaluationTemplateCommandService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<Guid>> CreateAsync(CreateTemplateRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var template = new EvaluationTemplate(request.Title, request.Description);
                foreach (var sectionDto in request.Sections)
                {
                    var section = new Section(sectionDto.Name, sectionDto.Weight , template.Id);

                    foreach (var criteriaDto in sectionDto.Criteria)
                    {
                        var criteria = new Criteria(criteriaDto.Name, criteriaDto.Description, criteriaDto.Weight, section.Id);
                        section.AddCriteria(criteria);
                    }

                    template.AddSection(section);
                }

                await _unitOfWork.EvaluationTemplates.AddAsync(template);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created Evaluation Template {TemplateId} with {SectionCount} sections.", template.Id, template.Sections.Count);
                transaction.Commit();

                return new ApiResponseDto<Guid>
                {
                    Message = "Evaluation template created successfully.",
                    Data = template.Id
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Error occurred while Creating Evaluation Template {TemplateTitle}", request.Title);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeactivateAsync(Guid id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var template = await _unitOfWork.EvaluationTemplates.GetByIdWithDetailsAsync(id);
                if (template == null)
                {
                    _logger.LogWarning("Attempted to deactivate non-existent Evaluation Template {TemplateId}.", id);
                    throw new KeyNotFoundException($"Evaluation Template with ID '{id}' does not exist.");
                }

                template.Deactivate();

                _unitOfWork.EvaluationTemplates.Update(template);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deactivated Evaluation Template {TemplateId}.", id);
                transaction.Commit();

                return new ApiResponseDto<string>
                {
                    Message = "Evaluation template deactivated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Error occurred while Deactivating Evaluation Template {TemplateId}", id);
                throw;
            }
        }
    }
}
