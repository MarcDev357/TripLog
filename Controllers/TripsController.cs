using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTripLog.Data;
using MyTripLog.Models;
using MyTripLog.ViewModels;

namespace MyTripLog.Controllers
{
    public class TripsController : Controller
    {
        private readonly MyTripLogContext _context;

        public TripsController(MyTripLogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trips = await _context.Trips
                .Include(t => t.Destination)
                .Include(t => t.Accommodation)
                .Include(t => t.TripActivities)
                    .ThenInclude(ta => ta.Activity)
                .ToListAsync();

            return View(trips);
        }

        public async Task<IActionResult> Add()
        {
            var vm = new AddTripViewModel
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

            vm.Destinations = await _context.Destinations
                .OrderBy(d => d.Name)
                .Select(d => new SelectListItem(d.Name, d.DestinationId.ToString()))
                .ToListAsync();

            vm.Accommodations = await _context.Accommodations
                .OrderBy(a => a.Name)
                .Select(a => new SelectListItem(a.Name, a.AccommodationId.ToString()))
                .ToListAsync();

            vm.Activities = await _context.Activities
                .OrderBy(a => a.Name)
                .Select(a => new SelectListItem(a.Name, a.ActivityId.ToString()))
                .ToListAsync();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTripViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return await Add();
            }

            var trip = new Trip
            {
                DestinationId = vm.DestinationId,
                AccommodationId = vm.AccommodationId,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate
            };

            foreach (var activityId in vm.SelectedActivityIds.Distinct())
            {
                trip.TripActivities.Add(new TripActivity
                {
                    ActivityId = activityId
                });
            }

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var trip = await _context.Trips
                .Include(t => t.TripActivities)
                .FirstOrDefaultAsync(t => t.TripId == id);

            if (trip == null)
                return NotFound();

            _context.TripActivities.RemoveRange(trip.TripActivities);
            _context.Trips.Remove(trip);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}