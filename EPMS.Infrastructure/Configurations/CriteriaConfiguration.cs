using EPMS.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPMS.Infrastructure.Configurations
{
    public class CriteriaConfiguration : IEntityTypeConfiguration<Criteria>
    {
        public void Configure(EntityTypeBuilder<Criteria> builder)
        {
            builder.ToTable("Criteria");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(c => c.Weight)
                .HasPrecision(5, 2)
                .IsRequired();
        }
    }
}
