using Mission08_Group4_6.Models;

public interface ITaskRepository
{
    IEnumerable<NewTask> GetAllTasks();
    NewTask GetTaskById(int id);
    void Add(NewTask task);
    void Update(NewTask task);
    void Delete(int id);
    void Save(); // Make sure this method exists
}
