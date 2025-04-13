using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class PlayerAwardController : Controller
    {
        private readonly IPlayerAwardService _service;

        public PlayerAwardController(IPlayerAwardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int playerId, int awardId)
        {
            var item = await _service.GetByIdAsync(playerId, awardId);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(PlayerAwardCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int playerId, int awardId)
        {
            var item = await _service.GetByIdAsync(playerId, awardId);
            if (item == null) return NotFound();

            var dto = new PlayerAwardUpdateDto
            {
                IsWinner = item.IsWinner
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int playerId, int awardId, PlayerAwardUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(playerId, awardId, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int playerId, int awardId)
        {
            var item = await _service.GetByIdAsync(playerId, awardId);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int playerId, int awardId)
        {
            await _service.DeleteAsync(playerId, awardId);
            return RedirectToAction(nameof(Index));
        }
    }
}