using InlandMarinaClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InlandMarinaLTD.Controllers
{
    public class LeaseController : Controller
    {
        private readonly InlandMarinaContext _context;
        public LeaseController(InlandMarinaContext context)
        {
            _context = context;
        }

        // GET lease
        public async Task<IActionResult> Index()
        {
            var availableSlips = await _context.Slips.Where(s => s.IsAvailable).ToListAsync();
            return View(availableSlips);
        }

        // POST lease
        [HttpPost]
        public async Task<IActionResult> LeaseSlip(int slipId)
        {
            var customerId = 1; // For now, assuming the current customer ID is 1
            var slip = await _context.Slips.FirstOrDefaultAsync(s => s.ID == slipId && s.IsAvailable);

            if (slip == null)
            {
                return NotFound();
            }

            // Lease the slip by adding a record to the Lease table
            var lease = new Lease
            {
                SlipID = slip.ID,
                CustomerID = customerId
            };

            _context.Leases.Add(lease);
            await _context.SaveChangesAsync();

            // Mark the slip as unavailable after leasing
            slip.IsAvailable = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Slip));
        }
    }
}

