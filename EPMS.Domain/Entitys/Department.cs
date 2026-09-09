namespace EPMS.Domain.Entitys
{
    public class Department : BaseEntity
    {
        public string Name { get;protected set; }
        public string Description { get;protected set; }

        private readonly List<Employee> _employees = new();
        public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();
        public Department(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("description is required.", nameof(description));
            Name = name;
            Description = description;
        }
        public bool UpdateDetails(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("description is required.", nameof(description));
            Name = name;
            Description = description;
            return true;
        }
        public bool AddEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException("employee Is required");
            Employees.Append(employee);
            return true;
        }
    }
}
