using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace EPMS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepository Employees { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public IEvaluationTemplateRepository EvaluationTemplates { get; private set; }
        public IEvaluationRepository Evaluations { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Employees = new EmployeeRepository(_context);
            Departments = new DepartmentRepository(_context);
            EvaluationTemplates = new EvaluationTemplateRepository(_context);
            Evaluations = new EvaluationRepository(_context);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
