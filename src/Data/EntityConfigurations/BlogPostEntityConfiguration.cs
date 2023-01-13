using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations {
    public class BlogPostEntityConfiguration : IEntityTypeConfiguration<BlogPost> {
        public void Configure(EntityTypeBuilder<BlogPost> builder) {
            builder.Property(x => x.AuthorID).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(64);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(32);
            builder.Property(x => x.BannerImageName).HasMaxLength(128);

            builder.HasIndex(x => x.Slug).IsUnique();

            builder.HasOne(p => p.Author)
                   .WithMany(u => u.PublishedPosts)
                   .HasForeignKey(p => p.AuthorID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.SupervisedBy)
                .WithMany(u => u.SupervisedPosts)
                .HasForeignKey(p => p.SupervisedByID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Tags)
                .WithMany()
                .UsingEntity(j => j.ToTable("PostTags"));

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryID);

            builder.UseTphMappingStrategy();
        }
    }
}
