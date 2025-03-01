using Mission08_Group4_6.Models;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public IEnumerable<NewTask> GetAllTasks()
    {
        return _context.Tasks.ToList();
    }

    public NewTask GetTaskById(int id)
    {
        return _context.Tasks.Find(id);
    }

    public void Add(NewTask task)
    {
        _context.Tasks.Add(task);
    }

    public void Update(NewTask task)
    {
        _context.Tasks.Update(task);
    }

    public void Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
        }
    }

    public void Save() // Implement Save() to match the interface
    {
        _context.SaveChanges();
    }
}
