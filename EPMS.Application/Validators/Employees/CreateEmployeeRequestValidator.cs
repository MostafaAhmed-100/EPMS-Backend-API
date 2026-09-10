using EPMS.Application.DTOs.EmployeeDTOs;
using FluentValidation;
namespace EPMS.Application.Validators.Employees
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("Job title is required.")
                .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.")
                .NotEqual(Guid.Empty).WithMessage("A valid Department ID must be provided.");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("Hire date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Hire date cannot be in the future.");
        }
    }
}
