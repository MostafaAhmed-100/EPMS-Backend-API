using EPMS.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPMS.Infrastructure.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("Sections");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.Weight)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Navigation(s => s.Criteria)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(s => s.Criteria)
                .WithOne(c => c.Section)
                .HasForeignKey(c => c.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
