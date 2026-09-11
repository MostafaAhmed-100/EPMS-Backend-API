using EPMS.Domain.Entitys;
namespace EPMS.Domain.Interfaces
{
    public interface IEvaluationTemplateRepository
    {
        Task<EvaluationTemplate?> GetByIdWithDetailsAsync(Guid id);
        Task<IReadOnlyList<EvaluationTemplate>> GetActiveTemplatesAsync();
        Task AddAsync(EvaluationTemplate template);
        void Update(EvaluationTemplate template);
    }
}
