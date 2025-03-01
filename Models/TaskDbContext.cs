using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mission08_Group4_6.Models;

namespace Mission08_Group4_6.Models
{
    public class TaskDbContext : DbContext
    {
        public DbSet<NewTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; } // Add this line
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This links CategoryId to the different names of the categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Home" },
                new Category { Id = 2, Name = "School" },
                new Category { Id = 3, Name = "Work" },
                new Category { Id = 4, Name = "Church" }

            );
        }
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
    }
    
}
