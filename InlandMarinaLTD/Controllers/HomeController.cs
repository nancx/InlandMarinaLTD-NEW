using InlandMarinaLTD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InlandMarinaClasses;


namespace InlandMarinaLTD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InlandMarinaContext _context;


        public HomeController(ILogger<HomeController> logger, InlandMarinaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Lease()
        {
            return RedirectToAction("Lease", "Slip");
        }

        public IActionResult MySlips()
        {
            return RedirectToAction("MySlips", "Slip");
        }

        public IActionResult Slip()
        {
            List<Slip> slips = _context.Slips.ToList();
            List<Dock> docks = _context.Docks.ToList();

            foreach (var slip in slips)
            {
                slip.Dock = docks.FirstOrDefault(d => d.ID == slip.DockID);
            }

            return View(slips);
        }

        [HttpPost]
        public IActionResult Slip(int id)
        {
            List<Slip> slips = _context.Slips.ToList();
            List<Dock> docks = _context.Docks.ToList();

            foreach (var slip in slips)
            {
                slip.Dock = docks.FirstOrDefault(d => d.ID == slip.DockID);
            }

            return View(slips);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}