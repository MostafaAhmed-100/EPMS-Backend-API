using EPMS.Domain.Entitys;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace EPMS.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Department?> GetByIdAsync(Guid id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            await _context.Departments.AddAsync(department);
        }

        public void Update(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            _context.Departments.Update(department);
        }
    }
}
