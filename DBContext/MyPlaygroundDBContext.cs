using Microsoft.EntityFrameworkCore;

namespace MyPlayground.DBContext
{
    public class MyPlaygroundDbContext : DbContext
    {
        private readonly string _connectionString;
        public MyPlaygroundDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DbSet<CitiesList> CitiesList { get; set; }
    }
}