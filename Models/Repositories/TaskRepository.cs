using Mission08_Group4_6.Models;
using System.Collections.Generic;
using System.Linq;  // Don't forget to include this for ToList() to work

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    // Implement GetAllTasks method
    public IEnumerable<NewTask> GetAllTasks()
    {
        return _context.Tasks.ToList();
    }

    // Implement GetTaskById method
    public NewTask? GetTaskById(int id)
    {
        return _context.Tasks.Find(id) ?? throw new InvalidOperationException("Task not found");
    }


    // Implement Add method
    public void Add(NewTask task)
    {
        _context.Tasks.Add(task);
    }

    // Implement Update method
    public void Update(NewTask task)
    {
        _context.Tasks.Update(task);
    }

    // Implement Delete method
    public void Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
        }
    }

    // Implement Save method
    public void Save()
    {
        _context.SaveChanges();
    }

    // Implement GetAllCategories method
    public IEnumerable<Category> GetAllCategories()
    {
        return _context.Categories.ToList(); // Fetch all categories from the database
    }
}
