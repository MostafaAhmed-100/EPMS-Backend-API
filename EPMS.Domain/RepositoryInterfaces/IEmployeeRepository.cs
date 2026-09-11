using EPMS.Domain.Entitys;
namespace EPMS.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Employee>> GetAllAsync();
        Task<IReadOnlyList<Employee>> GetByDepartmentIdAsync(Guid departmentId);
        Task AddAsync(Employee employee);
        void Update(Employee employee);
    }
}
