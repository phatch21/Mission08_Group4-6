using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Group4_6.Models;
using System.Linq;

namespace Mission08_Group4_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public HomeController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // ? Display Task List
        public IActionResult Index()
        {
            var tasks = _taskRepository.GetAllTasks();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult AddEditTask(int? id)
        {
            // Use the task repository, no need for _context
            if (id == null || id == 0)
            {
                return View(new NewTask()); // Creating a new task
            }

            var task = _taskRepository.GetTaskById(id.Value);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        public IActionResult AddEditTask(NewTask model)
        {
            if (ModelState.IsValid) // Validate the form input
            {
                if (model.TaskId == 0)
                {
                    _taskRepository.Add(model); // Add new task
                }
                else
                {
                    _taskRepository.Update(model); // Update existing task
                }

                _taskRepository.Save();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // ? Delete Task (GET Confirmation)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _taskRepository.GetTaskById(id); // Use _taskRepository
            if (recordToDelete == null)
            {
                return NotFound();
            }

            return View(recordToDelete);
        }

        // ? Confirm and Perform Delete
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var recordToDelete = _taskRepository.GetTaskById(id); // Use _taskRepository
            if (recordToDelete != null)
            {
                _taskRepository.Delete(id); // Use _taskRepository
                _taskRepository.Save();
            }

            return RedirectToAction("Index");
        }

        // ? Checkoff Task (Mark as Completed)
        public IActionResult Checkoff(int id)
        {
            var task = _taskRepository.GetTaskById(id); // Use _taskRepository

            if (task != null)
            {
                task.Completed = true;
                _taskRepository.Update(task); // Save the update
                _taskRepository.Save();
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
