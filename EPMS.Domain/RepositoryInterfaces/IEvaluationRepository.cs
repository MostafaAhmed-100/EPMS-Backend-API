using EPMS.Domain.Entitys;
namespace EPMS.Domain.Interfaces
{
    public interface IEvaluationRepository
    {
        Task<Evaluation?> GetByIdWithResponsesAsync(Guid id);
        Task<IReadOnlyList<Evaluation>> GetByEmployeeIdAsync(Guid employeeId);
        Task<IReadOnlyList<Evaluation>> GetByEvaluatorIdAsync(Guid evaluatorId);
        Task AddAsync(Evaluation evaluation);
        void Update(Evaluation evaluation);
    }
}
