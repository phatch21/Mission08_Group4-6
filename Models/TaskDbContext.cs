using Microsoft.EntityFrameworkCore;

namespace Mission08_Group4_6.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<NewTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; } // Ensures the Categories table exists

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Home" },
                new Category { Id = 2, Name = "School" },
                new Category { Id = 3, Name = "Work" },
                new Category { Id = 4, Name = "Church" }
            );
        }
    }
}
