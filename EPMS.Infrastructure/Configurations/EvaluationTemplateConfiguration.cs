using EPMS.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPMS.Infrastructure.Configurations
{
    public class EvaluationTemplateConfiguration : IEntityTypeConfiguration<EvaluationTemplate>
    {
        public void Configure(EntityTypeBuilder<EvaluationTemplate> builder)
        {
            builder.ToTable("EvaluationTemplates");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedNever();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(t => t.IsActive)
                .IsRequired();

            builder.Navigation(t => t.Sections)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            
            builder.HasMany(t => t.Sections)
                .WithOne(s => s.Template)
                .HasForeignKey(s => s.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
