using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class VenueTeamController : Controller
    {
        private readonly IVenueTeamService _service;

        public VenueTeamController(IVenueTeamService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int venueId, int teamId)
        {
            var item = await _service.GetByIdAsync(venueId, teamId);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VenueTeamCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int venueId, int teamId)
        {
            var item = await _service.GetByIdAsync(venueId, teamId);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int venueId, int teamId)
        {
            await _service.DeleteAsync(venueId, teamId);
            return RedirectToAction(nameof(Index));
        }
    }
}