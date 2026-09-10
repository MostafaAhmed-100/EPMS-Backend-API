using EPMS.Application.DTOs.EvaluationDTOs;

using FluentValidation;
namespace EPMS.Application.Validators.Evaluations
{
    public class CriterionRequestValidator : AbstractValidator<CriterionResponseItemRequest>
    {
        public CriterionRequestValidator()
        {
            RuleFor(x => x.CriteriaId)
                .NotEmpty().WithMessage("Criteria ID is required.")
                .NotEqual(Guid.Empty).WithMessage("A valid Criteria ID must be provided.");

            RuleFor(x => x.Score)
                .InclusiveBetween(1, 5).WithMessage("Score must be between 1 and 5.");

            RuleFor(x => x.Feedback)
                .MaximumLength(1000).WithMessage("Feedback cannot exceed 1000 characters.");
        }
    }
}
