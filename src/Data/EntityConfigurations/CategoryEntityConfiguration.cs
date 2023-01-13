using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations {
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category> {
        public void Configure(EntityTypeBuilder<Category> builder) {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
