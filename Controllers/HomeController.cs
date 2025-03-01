using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Group4_6.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mission08_Group4_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskDbContext _context; // Injecting DB Context

        public HomeController(TaskDbContext context)
        {
            _context = context;
        }

        // ? Display Task List
        public IActionResult Index()
        {
            var tasks = _context.Tasks?.ToList() ?? new List<NewTask>(); // Remove Category reference
            return View(tasks);
        }

        [HttpGet]
        public IActionResult AddEditTask(int? id)
        {
            
            ViewBag.Categories = _context.Categories.ToList(); // Reload categories if validation fails

            if (id == null || id == 0)
            {
                return View(new NewTask()); // Creating a new task
            }

            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task); // Editing an existing task
        }

        // ? Save (Add or Update) Task
        [HttpPost]
        public IActionResult AddEditTask(NewTask model)
        {
            if (ModelState.IsValid) // Validate the form input
            {
                if (model.Id == 0)
                {
                    _context.Tasks.Add(model); // Add new task
                }
                else
                {
                    _context.Tasks.Update(model); // Update existing task
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            var taskToEdit = _context.Tasks
                .Include(x => x.Category)
                .Single(x => x.Id == id);  
            
            var tasks = _context.Tasks.ToList();
            
            ViewBag.Categories = _context.Categories.ToList();
            
            return View("Index", tasks);
            
        }

        [HttpPost]
        public IActionResult Edit(NewTask model)
        {
            _context.Tasks.Update(model);
            _context.SaveChanges();
            

            return RedirectToAction("Index");
        }
        
        

        // ? Delete Task (GET Confirmation)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _context.Tasks.Find(id);
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
            var recordToDelete = _context.Tasks.Find(id);
            if (recordToDelete != null)
            {
                _context.Tasks.Remove(recordToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // ? Checkoff Task (Mark as Completed)
        public IActionResult Checkoff(int id)
        {
            var task = _context.Tasks.Find(id); // Use Find() for efficiency

            if (task != null)
            {
                task.Completed = true; // Update the Completed field
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
    }
}
