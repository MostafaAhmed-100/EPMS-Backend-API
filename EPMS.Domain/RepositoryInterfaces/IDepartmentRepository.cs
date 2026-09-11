using EPMS.Domain.Entitys;
namespace EPMS.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Department>> GetAllAsync();
        Task AddAsync(Department department);
        void Update(Department department);
    }
}
