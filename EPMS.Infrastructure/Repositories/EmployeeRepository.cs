using EPMS.Domain.Entitys;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IReadOnlyList<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Employee>> GetByDepartmentIdAsync(Guid departmentId)
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(e => e.Department)
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            await _context.Employees.AddAsync(employee);
        }

        public void Update(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employees.Update(employee);
        }
    }
}
