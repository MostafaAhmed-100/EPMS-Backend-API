using EPMS.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPMS.Infrastructure.Configurations
{
    public class EvaluationConfiguration : IEntityTypeConfiguration<Evaluation>
    {
        public void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            builder.ToTable("Evaluations");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.FinalScore)
                .HasPrecision(5, 2);

            // تكوين الـ Value Object (DateRange) كـ Owned Type
            builder.OwnsOne(e => e.Period, period =>
            {
                period.Property(p => p.StartDate)
                    .HasColumnName("StartDate")
                    .IsRequired();

                period.Property(p => p.EndDate)
                    .HasColumnName("EndDate")
                    .IsRequired();
            });

            builder.HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Evaluator)
                .WithMany()
                .HasForeignKey(e => e.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey(e => e.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            // ضبط الـ Backing Field الخاص بقائمة الردود
            builder.Navigation(e => e.Responses)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(e => e.Responses)
                .WithOne(r => r.Evaluation)
                .HasForeignKey(r => r.EvaluationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
