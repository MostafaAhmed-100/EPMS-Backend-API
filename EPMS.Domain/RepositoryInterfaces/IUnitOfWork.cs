using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace EPMS.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        IEvaluationTemplateRepository EvaluationTemplates { get; }
        IEvaluationRepository Evaluations { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}
