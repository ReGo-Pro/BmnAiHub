using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityConfigurations {
    public class TagEntityConfiguration : IEntityTypeConfiguration<Tag> {
        public void Configure(EntityTypeBuilder<Tag> builder) {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
