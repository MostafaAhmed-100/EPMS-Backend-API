using EPMS.Domain.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EPMS.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<EvaluationTemplate> EvaluationTemplates => Set<EvaluationTemplate>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Criteria> Criteria => Set<Criteria>();
        public DbSet<Evaluation> Evaluations => Set<Evaluation>();
        public DbSet<EvaluationResponse> EvaluationResponses => Set<EvaluationResponse>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
