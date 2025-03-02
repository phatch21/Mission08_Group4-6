// Emma Helquist, Payton Hatch, Tessa Miner, Addison Smith
// Group 4-6

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Group4_6.Models;

namespace Mission08_Group4_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        // Constructor
        public HomeController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // Display Task List, main page
        public IActionResult Index()
        {
            var tasks = _taskRepository.GetAllTasks().ToList();
            return View(tasks);
        }

        // Get for adding a new task
        [HttpGet]
        public IActionResult AddEditTask(int? id)
        {

            ViewBag.Categories = _taskRepository.GetAllCategories()
                .Select(c => new { Id = c.Id, Name = c.Name }) // Ensuring only relevant properties
                .ToList();

            if (id == null || id == 0)
            {
                return View(new NewTask());
            }

            var task = _taskRepository.GetTaskById(id.Value);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        
        // Post for adding a new task
        [HttpPost]
        public IActionResult AddEditTask(NewTask model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _taskRepository.Add(model);
                }
                else
                {
                    _taskRepository.Update(model);
                }

                _taskRepository.Save();
                return RedirectToAction("Index");
            }

            // Repopulate categories if validation fails
            ViewBag.Categories = _taskRepository.GetAllCategories()
                .Select(c => new { Id = c.Id, Name = c.Name })
                .ToList();

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Get to edit a task
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var taskToEdit = _taskRepository.GetTaskById(id);

            if (taskToEdit == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _taskRepository.GetAllCategories().ToList();

            return View("AddEditTask", taskToEdit);
        }
        
        // Post to update an edited task
        [HttpPost]
        public IActionResult Edit(NewTask model)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.Update(model);
                _taskRepository.Save();
            }

            return RedirectToAction("Index");
        }
        
        // Delete Task (GET)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _taskRepository.GetTaskById(id);
            if (recordToDelete == null)
            {
                return NotFound();
            }

            return View(recordToDelete); // Ensure Delete.cshtml exists
        }


        // Confirm and Perform Delete
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var recordToDelete = _taskRepository.GetTaskById(id);

            if (recordToDelete == null)
            {
                return NotFound(); // Handle case where task is already deleted
            }

            _taskRepository.Delete(id);
            _taskRepository.Save();

            return RedirectToAction("Index");
        }


        // Checkoff Task (Mark as Completed)
        public IActionResult Checkoff(int id)
        {
            var task = _taskRepository.GetTaskById(id); 

            if (task != null)
            {
                task.Completed = true;
                _taskRepository.Update(task); 
                _taskRepository.Save();
            }

            return RedirectToAction("Index");
        }

    }
}
