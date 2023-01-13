using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityConfigurations {
    public class UserEntityConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(11)
                .IsFixedLength(true);

            builder.Property(x => x.FirstName)
                .HasMaxLength(128);

            builder.Property(x => x.LastName)
                .HasMaxLength(128);

            builder.Property(x => x.NationalCode)
                .HasMaxLength(10)
                .IsFixedLength(true);

            builder.Property(x => x.ProfilePictureName)
                .HasMaxLength(128);
        }
    }
}
