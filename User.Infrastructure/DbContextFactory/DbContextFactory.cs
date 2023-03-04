using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace User.Infrastructure.DbContextFactory
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<UserDbContext> builder = new DbContextOptionsBuilder<UserDbContext>();
            //var connStr = Environment.GetEnvironmentVariable("ConnStr");
            var connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DDDDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            builder.UseSqlServer(connStr);
            builder.LogTo(Console.WriteLine);
            return new UserDbContext(builder.Options);
        }
    }
}
