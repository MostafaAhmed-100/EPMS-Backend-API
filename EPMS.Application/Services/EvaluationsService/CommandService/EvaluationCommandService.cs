using AutoMapper;
using EPMS.Application.DTOs.EvaluationDTOs;
using EPMS.Application.DTOs.Shared;
using EPMS.Domain.Entitys;
using EPMS.Domain.Interfaces;
using EPMS.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace EPMS.Application.Services.EvaluationsService.CommandService
{
    public class EvaluationCommandService : IEvaluationCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EvaluationCommandService> _logger;

        public EvaluationCommandService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<EvaluationCommandService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<Guid>> InitiateEvaluationAsync(InitiateEvaluationRequest request)
        {
            try
            {
                if (request.EmployeeId == request.EvaluatorId)
                {
                    _logger.LogWarning("Evaluation initiation rejected. Employee {EmployeeId} cannot evaluate themselves.", request.EmployeeId);
                    throw new InvalidOperationException("An employee cannot evaluate themselves.");
                }

                var employee = await _unitOfWork.Employees.GetByIdAsync(request.EmployeeId);
                if (employee == null)
                {
                    _logger.LogWarning("Employee {EmployeeId} not found.", request.EmployeeId);
                    throw new KeyNotFoundException($"Employee with ID '{request.EmployeeId}' does not exist.");
                }

                var evaluator = await _unitOfWork.Employees.GetByIdAsync(request.EvaluatorId);
                if (evaluator == null)
                {
                    _logger.LogWarning("Evaluator {EvaluatorId} not found.", request.EvaluatorId);
                    throw new KeyNotFoundException($"Evaluator with ID '{request.EvaluatorId}' does not exist.");
                }

                var template = await _unitOfWork.EvaluationTemplates.GetByIdWithDetailsAsync(request.TemplateId);
                if (template == null)
                {
                    _logger.LogWarning("Evaluation template {TemplateId} not found.", request.TemplateId);
                    throw new KeyNotFoundException($"Evaluation template with ID '{request.TemplateId}' does not exist.");
                }

                if (!template.IsActive)
                {
                    _logger.LogWarning("Evaluation template {TemplateId} is inactive.", request.TemplateId);
                    throw new InvalidOperationException("Cannot initiate evaluation using an inactive template.");
                }

                var period = new DateRange(request.StartDate, request.EndDate);
                var evaluation = new Evaluation(request.EmployeeId, request.EvaluatorId, request.TemplateId, period);

                await _unitOfWork.Evaluations.AddAsync(evaluation);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully initiated evaluation {EvaluationId} for employee {EmployeeId}.", evaluation.Id, request.EmployeeId);

                return new ApiResponseDto<Guid>
                {
                    Message = "Evaluation initiated successfully.",
                    Data = evaluation.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Initiating Evaluation for Employee {EmployeeId}", request.EmployeeId);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> SubmitScoresAsync(Guid evaluationId, SubmitEvaluationResponsesRequest request) 
        { 
            try
            {
                var evaluation = await _unitOfWork.Evaluations.GetByIdWithResponsesAsync(evaluationId);
                if (evaluation == null)
                {
                    _logger.LogWarning("Evaluation {EvaluationId} not found.", evaluationId);
                    throw new KeyNotFoundException($"Evaluation with ID '{evaluationId}' does not exist.");
                }

                var template = await _unitOfWork.EvaluationTemplates.GetByIdWithDetailsAsync(evaluation.TemplateId);
                if (template == null)
                {
                    _logger.LogWarning("Associated template {TemplateId} for evaluation {EvaluationId} not found.", evaluation.TemplateId, evaluationId);
                    throw new KeyNotFoundException("Associated evaluation template does not exist.");
                }

                var templateCriteriaIds = template.Sections
                    .SelectMany(s => s.Criteria)
                    .Select(c => c.Id)
                    .ToHashSet();

                foreach (var responseDto in request.Responses)
                {
                    if (!templateCriteriaIds.Contains(responseDto.CriteriaId))
                    {
                        _logger.LogWarning("Criteria {CriteriaId} does not belong to Template {TemplateId}.", responseDto.CriteriaId, template.Id);
                        throw new InvalidOperationException($"Criteria '{responseDto.CriteriaId}' does not belong to the evaluation template.");
                    }

                    var response = new EvaluationResponse(evaluationId, responseDto.CriteriaId, responseDto.Score, responseDto.Feedback);
                    evaluation.AddResponse(response);
                }

                evaluation.CalculateFinalScore(template);
                evaluation.CompleteEvaluation();

                _unitOfWork.Evaluations.Update(evaluation);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully submitted scores and completed evaluation {EvaluationId}.", evaluationId);

                return new ApiResponseDto<string>
                {
                    Message = "Evaluation scores submitted and evaluation completed successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Submitting Scores for Evaluation {EvaluationId}", evaluationId);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> CancelEvaluationAsync(Guid evaluationId)
        {
            try
            {
                var evaluation = await _unitOfWork.Evaluations.GetByIdWithResponsesAsync(evaluationId);
                if (evaluation == null)
                {
                    _logger.LogWarning("Evaluation {EvaluationId} not found.", evaluationId);
                    throw new KeyNotFoundException($"Evaluation with ID '{evaluationId}' does not exist.");
                }

                evaluation.CancelEvaluation();

                _unitOfWork.Evaluations.Update(evaluation);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully cancelled evaluation {EvaluationId}.", evaluationId);

                return new ApiResponseDto<string>
                {
                    Message = "Evaluation cancelled successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Cancelling Evaluation {EvaluationId}", evaluationId);
                throw;
            }
        }
    }
}
