using EPMS.Application.DTOs.EmployeeDTOs;
using FluentValidation;
namespace EPMS.Application.Validators.Employees
{
    public class UpdateEmployeeDepartmentRequestValidator : AbstractValidator<UpdateEmployeeDepartmentRequest>
    {
        public UpdateEmployeeDepartmentRequestValidator()
        {
            RuleFor(x => x.NewDepartmentId)
                .NotEmpty().WithMessage("New Department ID is required.")
                .NotEqual(Guid.Empty).WithMessage("A valid Department ID must be provided.");
        }
    }
}
