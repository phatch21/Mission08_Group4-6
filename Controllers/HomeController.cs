using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Group4_6.Models;

namespace Mission08_Group4_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
        public IActionResult Edit(Task updatedTask)
        {
            _context.Update(updatedTask);
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
        public IActionResult Delete(Task recordToDelete)
        {
            _context.Tasks.Remove(recordToDelete);
            _context.SaveChanges();
        
            return RedirectToAction("Index");
        }
    }
}
