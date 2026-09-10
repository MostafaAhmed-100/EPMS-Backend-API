using EPMS.Application.DTOs.DepartmentDTOs;
using FluentValidation;
namespace EPMS.Application.Validators.Departments
{
    public class CreateDepartmentRequestValidator : AbstractValidator<CreateDepartmentRequest>
    {
        public CreateDepartmentRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required.")
                .MaximumLength(100).WithMessage("Department name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
