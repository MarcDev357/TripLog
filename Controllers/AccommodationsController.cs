using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTripLog.Data;
using MyTripLog.Models;

namespace MyTripLog.Controllers
{
    public class AccommodationsController : Controller
    {
        private readonly MyTripLogContext _context;

        public AccommodationsController(MyTripLogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var accommodations = await _context.Accommodations
                .OrderBy(a => a.Name)
                .ToListAsync();

            return View(accommodations);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name, string? phone, string? email)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _context.Accommodations.Add(new Accommodation
                {
                    Name = name.Trim(),
                    Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim(),
                    Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim()
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);

            if (accommodation == null)
                return NotFound();

            try
            {
                _context.Accommodations.Remove(accommodation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "Cannot delete an accommodation that is associated with a trip.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}