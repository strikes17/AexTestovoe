using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Manager> Managers => Set<Manager>();
        public DbSet<Order> Orders => Set<Order>();

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=store.db");
        }
    }
}