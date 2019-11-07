using Microsoft.EntityFrameworkCore;
namespace TaskDB
{
    class Context : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<User> Users {get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        =>optionsBuilder
        .UseNpgsql("Host=localhost;Database=task_db;Username=postgres;Password=123");
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new JobMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}