using EPMS.Domain.Entitys;
namespace EPMS.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Employee>> GetByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default);
        Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
        void Update(Employee employee);
    }
}
