namespace EPMS.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        IEvaluationTemplateRepository EvaluationTemplates { get; }
        IEvaluationRepository Evaluations { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
