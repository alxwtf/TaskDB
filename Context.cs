using Microsoft.EntityFrameworkCore;
namespace TaskDB
{
    class Context : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        =>optionsBuilder
        .UseNpgsql("Host=localhost;Database=task_db;Username=postgres;Password=123");
    }
}