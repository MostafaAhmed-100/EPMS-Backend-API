namespace EPMS.Domain.Entitys
{
    public class Employee : User
    {
        public string JobTitle { get; private set; }
        public DateTime HireDate { get; private set; }
        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }

        public Employee(string firstName, string lastName, string jobTitle, DateTime hireDate, Guid departmentId)
            : base(firstName, lastName)
        {
            if (string.IsNullOrWhiteSpace(jobTitle))
                throw new ArgumentException("Job title is required.", nameof(jobTitle));

            if (departmentId == Guid.Empty)
                throw new ArgumentException("Valid department ID is required.", nameof(departmentId));

            if (hireDate > DateTime.UtcNow)
                throw new ArgumentException("Hire date cannot be in the future.", nameof(hireDate));

            JobTitle = jobTitle;
            HireDate = hireDate;
            DepartmentId = departmentId;
        }

        public void ChangeDepartment(Guid newDepartmentId)
        {
            if (newDepartmentId == Guid.Empty)
                throw new ArgumentException("Valid department ID is required.", nameof(newDepartmentId));

            if (DepartmentId == newDepartmentId)
                throw new InvalidOperationException("Employee is already assigned to this department.");

            DepartmentId = newDepartmentId;
        }

        public void UpdateJobTitle(string newJobTitle)
        {
            if (string.IsNullOrWhiteSpace(newJobTitle))
                throw new ArgumentException("Job title is required.", nameof(newJobTitle));

            JobTitle = newJobTitle;
        }
    }
}
