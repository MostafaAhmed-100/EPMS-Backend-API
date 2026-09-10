namespace EPMS.Application.DTOs.EmployeeDTOs
{
    public class CreateEmployeeRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
