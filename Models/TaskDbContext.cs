using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mission08_Group4_6.Models;

namespace Mission08_Group4_6.Models
{
    public class TaskDbContext : DbContext
    {
        public DbSet<NewTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; } // Add this line

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }
    }
}
