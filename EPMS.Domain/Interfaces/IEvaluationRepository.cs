using EPMS.Domain.Entitys;
namespace EPMS.Domain.Interfaces
{
    public interface IEvaluationRepository
    {
        Task<Evaluation?> GetByIdWithResponsesAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Evaluation>> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Evaluation>> GetByEvaluatorIdAsync(Guid evaluatorId, CancellationToken cancellationToken = default);
        Task AddAsync(Evaluation evaluation, CancellationToken cancellationToken = default);
        void Update(Evaluation evaluation);
    }
}
