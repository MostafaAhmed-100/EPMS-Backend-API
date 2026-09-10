using EPMS.Application.DTOs.EvaluationDTOs;
using FluentValidation;
namespace EPMS.Application.Validators.Evaluations
{
    public class InitiateEvaluationRequestValidator : AbstractValidator<InitiateEvaluationRequest>
    {
        public InitiateEvaluationRequestValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Employee ID is required.")
                .NotEqual(Guid.Empty).WithMessage("A valid Employee ID must be provided.");

            RuleFor(x => x.EvaluatorId)
                .NotEmpty().WithMessage("Evaluator ID is required.")
                .NotEqual(Guid.Empty).WithMessage("A valid Evaluator ID must be provided.")
                .NotEqual(x => x.EmployeeId).WithMessage("An employee cannot evaluate themselves.");

            RuleFor(x => x.TemplateId)
                .NotEmpty().WithMessage("Template ID is required.")
                .NotEqual(Guid.Empty).WithMessage("A valid Template ID must be provided.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be greater than start date.");
        }
    }
}
