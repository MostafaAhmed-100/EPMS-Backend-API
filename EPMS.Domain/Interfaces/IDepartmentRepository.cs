using EPMS.Domain.Entitys;
namespace EPMS.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Department department, CancellationToken cancellationToken = default);
        void Update(Department department);
    }
}
