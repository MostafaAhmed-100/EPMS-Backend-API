namespace EPMS.Application.DTOs.DepartmentDTOs
{
    public class DepartmentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EmployeesCount { get; set; }
    }
}
