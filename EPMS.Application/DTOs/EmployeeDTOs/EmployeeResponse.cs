namespace EPMS.Application.DTOs.EmployeeDTOs
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
