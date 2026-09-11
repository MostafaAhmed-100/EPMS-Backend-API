using EPMS.Domain.Entitys;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories
{
    public class EvaluationTemplateRepository : IEvaluationTemplateRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationTemplateRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<EvaluationTemplate?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.EvaluationTemplates
                .Include(t => t.Sections)
                    .ThenInclude(s => s.Criteria)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IReadOnlyList<EvaluationTemplate>> GetActiveTemplatesAsync()
        {
            return await _context.EvaluationTemplates
                .AsNoTracking()
                .Where(t => t.IsActive)
                .Include(t => t.Sections)
                    .ThenInclude(s => s.Criteria)
                .ToListAsync();
        }

        public async Task AddAsync(EvaluationTemplate template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            await _context.EvaluationTemplates.AddAsync(template);
        }

        public void Update(EvaluationTemplate template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            _context.EvaluationTemplates.Update(template);
        }
    }
}
