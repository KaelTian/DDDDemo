using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.Domain;
using DomainUser = User.Domain.User;


namespace User.Infrastructure
{
    public class UserDbContext:DbContext
    {

        public DbSet<DomainUser> Users { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
