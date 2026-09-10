using EPMS.Application.DTOs.EvaluationDTOs;
using FluentValidation;
namespace EPMS.Application.Validators.Evaluations
{
    public class SubmitEvaluationResponsesRequestValidator : AbstractValidator<SubmitEvaluationResponsesRequest>
    {
        public SubmitEvaluationResponsesRequestValidator()
        {
            RuleFor(x => x.Responses)
                .NotEmpty().WithMessage("Evaluation responses cannot be empty.");

            RuleForEach(x => x.Responses).SetValidator(new CriterionRequestValidator());
        }
    }
}
