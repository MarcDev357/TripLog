using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTripLog.Data;
using MyTripLog.Models;

namespace MyTripLog.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly MyTripLogContext _context;

        public DestinationsController(MyTripLogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var destinations = await _context.Destinations
                .OrderBy(d => d.Name)
                .ToListAsync();

            return View(destinations);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _context.Destinations.Add(new Destination { Name = name.Trim() });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
                return NotFound();

            try
            {
                _context.Destinations.Remove(destination);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "Cannot delete a destination that is associated with a trip.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}