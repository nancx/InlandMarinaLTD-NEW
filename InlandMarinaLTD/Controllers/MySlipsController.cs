using InlandMarinaClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InlandMarinaLTD.Controllers
{
    public class MySlipsController : Controller
    {
        private readonly InlandMarinaContext _context;

        public MySlipsController(InlandMarinaContext context)
        {
            _context = context;
        }

        // GET MySlips
        public async Task<IActionResult> Index()
        {
            var customerId = 1; // assuming the current customer ID is 1
            var mySlips = await _context.Leases
                .Where(l => l.CustomerID == customerId)
                .Select(l => l.Slip)
                .ToListAsync();

            return View(mySlips);
        }
    }
}
