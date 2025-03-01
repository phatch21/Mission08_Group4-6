using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Group4_6.Models;

namespace Mission08_Group4_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskDbContext _context; // Injecting DB Context

        public HomeController(TaskDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList(); // Fetch tasks from DB

            if (tasks == null) // Ensure Model is never null
            {
                tasks = new List<NewTask>();
            }

            return View(tasks);
        }


        public IActionResult AddEditTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEditTask(Models.NewTask model)
        {
            if (ModelState.IsValid) // Validate the form input
            {
                _context.Tasks.Add(model); // Save to the database
                _context.SaveChanges(); // Commit the changes
                return RedirectToAction("Index"); // Redirect to main page
            }

            return View(model); // Return view with errors if validation fails
        }

        public IActionResult Privacy()
        {
            return View();
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
        
        

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _context.Tasks
                .Single(x => x.Id == id);
            
            return View(recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(NewTask recordToDelete)
        {
            _context.Tasks.Remove(recordToDelete);
            _context.SaveChanges();
        
            return RedirectToAction("Index");
        }

        public IActionResult Checkoff(int id)
        {
            return View("Index");
        }
    }
}

