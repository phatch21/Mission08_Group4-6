using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
            return View();
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
    }
}
