using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class AwardController : Controller
    {
        private readonly IAwardService _awardService;
        private readonly IPlayerService _playerService;

        public AwardController(IAwardService awardService, IPlayerService playerService)
        {
            _awardService = awardService;
            _playerService = playerService;
        }

        public async Task<IActionResult> Index()
        {
            var awards = await _awardService.GetAllAwardsAsync();
            return View(awards);
        }

        public async Task<IActionResult> Details(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null) return NotFound();
            return View(award);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AwardCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _awardService.CreateAwardAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null) return NotFound();
            return View(new AwardUpdateDto
            {
                Name = award.Name,
                Description = award.Description,
                IsActive = award.IsActive
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AwardUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _awardService.UpdateAwardAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null) return NotFound();
            return View(award);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _awardService.DeleteAwardAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ✅ NOMINATE VIEW

        [HttpGet]
        public async Task<IActionResult> Nominate(int awardId)
        {
            var nominees = await _awardService.GetAwardNomineesAsync(awardId);
            var players = await _playerService.GetAllPlayersAsync();
            var award = await _awardService.GetAwardByIdAsync(awardId);

            ViewBag.Players = new SelectList(players, "PlayerId", "Name");
            ViewBag.AwardId = awardId;
            ViewBag.AwardName = award?.Name ?? "Award";

            return View(nominees);
        }

        [HttpPost]
        public async Task<IActionResult> Nominate(int awardId, int playerId)
        {
            if (!await _awardService.IsPlayerNominatedAsync(awardId, playerId))
                await _awardService.NominatePlayerAsync(awardId, playerId);

            return RedirectToAction(nameof(Nominate), new { awardId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveNomination(int awardId, int playerId)
        {
            await _awardService.RemoveNominationAsync(awardId, playerId);
            return RedirectToAction(nameof(Nominate), new { awardId });
        }
    }
}
