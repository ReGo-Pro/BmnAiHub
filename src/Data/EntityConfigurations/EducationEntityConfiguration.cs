using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations {
    public class EducationEntityConfiguration : IEntityTypeConfiguration<Education> {
        public void Configure(EntityTypeBuilder<Education> builder) {
            builder.Property(x => x.Level)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.Major)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.SubMajor)
                .HasMaxLength(128);

            builder.Property(x => x.Institute)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.GPA)
                .HasPrecision(4, 2);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Educations)
                .IsRequired()
                .HasForeignKey(e => e.UserId);
        }
    }
}
