using EPMS.Application.DTOs.TemplateDTOs;
using FluentValidation;
namespace EPMS.Application.Validators.Templates
{
    public class CreateCriteriaRequestValidator : AbstractValidator<CreateCriteriaRequest>
    {
        public CreateCriteriaRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Criteria name is required.")
                .MaximumLength(150).WithMessage("Criteria name must not exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Weight)
                .InclusiveBetween(1, 100).WithMessage("Criteria weight must be between 1 and 100.");
        }
    }
}
