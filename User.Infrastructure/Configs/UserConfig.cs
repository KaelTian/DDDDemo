using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain;
using DomainUser = User.Domain.User;

namespace User.Infrastructure
{
    public class UserConfig : IEntityTypeConfiguration<DomainUser>
    {
        public void Configure(EntityTypeBuilder<DomainUser> builder)
        {
            builder.ToTable("T_Users");
            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.Property(x => x.RegionNumber).HasMaxLength(5).IsUnicode(false);
                nb.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
            });
            builder.Property("passwordHash").HasMaxLength(100).IsUnicode(false);
            builder.HasOne(x => x.UserAccessFail).WithOne(x => x.User)
                .HasForeignKey<UserAccessFail>(x => x.UserId);
        }
    }
}
