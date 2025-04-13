using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        public async Task<IActionResult> Index()
        {
            var venues = await _venueService.GetAllVenuesAsync();
            return View(venues);
        }

        public async Task<IActionResult> Details(int id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            if (venue == null) return NotFound();
            return View(venue);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VenueCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _venueService.CreateVenueAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            if (venue == null) return NotFound();
            return View(new VenueUpdateDto { Name = venue.Name, City = venue.City, Capacity = venue.Capacity });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VenueUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _venueService.UpdateVenueAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            if (venue == null) return NotFound();
            return View(venue);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _venueService.DeleteVenueAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}