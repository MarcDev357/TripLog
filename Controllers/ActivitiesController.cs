using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTripLog.Data;
using MyTripLog.Models;

namespace MyTripLog.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly MyTripLogContext _context;

        public ActivitiesController(MyTripLogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var activities = await _context.Activities
                .OrderBy(a => a.Name)
                .ToListAsync();

            return View(activities);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _context.Activities.Add(new Activity
                {
                    Name = name.Trim()
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
                return NotFound();

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}