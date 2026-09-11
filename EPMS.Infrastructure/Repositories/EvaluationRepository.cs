using EPMS.Domain.Entitys;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Evaluation?> GetByIdWithResponsesAsync(Guid id)
        {
            return await _context.Evaluations
                .Include(e => e.Employee)
                .Include(e => e.Evaluator)
                .Include(e => e.Template)
                .Include(e => e.Responses)
                    .ThenInclude(r => r.Criteria)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IReadOnlyList<Evaluation>> GetByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.Evaluations
                .AsNoTracking()
                .Include(e => e.Evaluator)
                .Include(e => e.Template)
                .Where(e => e.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Evaluation>> GetByEvaluatorIdAsync(Guid evaluatorId)
        {
            return await _context.Evaluations
                .AsNoTracking()
                .Include(e => e.Employee)
                .Include(e => e.Template)
                .Where(e => e.EvaluatorId == evaluatorId)
                .ToListAsync();
        }

        public async Task AddAsync(Evaluation evaluation)
        {
            if (evaluation == null)
            {
                throw new ArgumentNullException(nameof(evaluation));
            }

            await _context.Evaluations.AddAsync(evaluation);
        }

        public void Update(Evaluation evaluation)
        {
            if (evaluation == null)
            {
                throw new ArgumentNullException(nameof(evaluation));
            }

            _context.Evaluations.Update(evaluation);
        }
    }
}
