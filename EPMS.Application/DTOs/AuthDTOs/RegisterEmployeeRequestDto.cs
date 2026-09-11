using EPMS.Application.Constants;

namespace EPMS.Application.DTOs.AuthDTOs
{
    public class RegisterEmployeeRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = AppRoles.Employee;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
