using EPMS.Application.DTOs.TemplateDTOs;
using FluentValidation;
namespace EPMS.Application.Validators.Templates
{
    public class CreateTemplateRequestValidator : AbstractValidator<CreateTemplateRequest>
    {
        public CreateTemplateRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Template title is required.")
                .MaximumLength(150).WithMessage("Template title must not exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Sections)
                .NotEmpty().WithMessage("Template must contain at least one section.")
                .Must(sections => sections != null && sections.Sum(s => s.Weight) == 100)
                .WithMessage("The total sum of section weights in a template must equal exactly 100%.");

            RuleForEach(x => x.Sections).SetValidator(new CreateSectionRequestValidator());
        }
    }
}
