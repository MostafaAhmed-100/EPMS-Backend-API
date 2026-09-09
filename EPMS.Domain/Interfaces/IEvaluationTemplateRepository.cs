using EPMS.Domain.Entitys;
namespace EPMS.Domain.Interfaces
{
    public interface IEvaluationTemplateRepository
    {
        Task<EvaluationTemplate?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<EvaluationTemplate>> GetActiveTemplatesAsync(CancellationToken cancellationToken = default);
        Task AddAsync(EvaluationTemplate template, CancellationToken cancellationToken = default);
        void Update(EvaluationTemplate template);
    }
}
