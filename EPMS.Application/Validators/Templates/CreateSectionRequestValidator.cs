using EPMS.Application.DTOs.TemplateDTOs;
using FluentValidation;
namespace EPMS.Application.Validators.Templates
{
    public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
    {
        public CreateSectionRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Section name is required.")
                .MaximumLength(100).WithMessage("Section name must not exceed 100 characters.");

            RuleFor(x => x.Weight)
                .InclusiveBetween(1, 100).WithMessage("Section weight must be between 1 and 100.");

            RuleFor(x => x.Criteria)
                .NotEmpty().WithMessage("Section must contain at least one criteria.")
                .Must(criteria => criteria != null && criteria.Sum(c => c.Weight) == 100)
                .WithMessage("The sum of criteria weights within a section must equal exactly 100%.");

            RuleForEach(x => x.Criteria).SetValidator(new CreateCriteriaRequestValidator());
        }
    }
}
