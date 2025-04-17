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
        private readonly IVoteService _voteService;

        public AwardController(
            IAwardService awardService,
            IPlayerService playerService,
            IVoteService voteService)
        {
            _awardService = awardService;
            _playerService = playerService;
            _voteService = voteService;
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
            var votes = await _voteService.GetAllVotesAsync();

            // Filter votes for this award
            var voteGroups = votes
                .Where(v => v.AwardId == awardId)
                .GroupBy(v => v.PlayerId)
                .ToDictionary(g => g.Key, g => g.Count());

            // Find max vote count
            var maxVotes = voteGroups.Any() ? voteGroups.Max(x => x.Value) : 0;

            // Set winner and vote count per nominee
            foreach (var nominee in nominees)
            {
                nominee.VotesReceived = voteGroups.ContainsKey(nominee.PlayerId)
                    ? voteGroups[nominee.PlayerId]
                    : 0;

                nominee.IsWinner = nominee.VotesReceived == maxVotes && maxVotes > 0;
            }

            ViewBag.Players = new SelectList(players, "PlayerId", "Name");
            ViewBag.AwardId = awardId;
            ViewBag.AwardName = award?.Name ?? "Award";

            return View(nominees);
        }

        // ✅ POST: Nominate a player
        [HttpPost]
        public async Task<IActionResult> Nominate(int awardId, int playerId)
        {
            if (!await _awardService.IsPlayerNominatedAsync(awardId, playerId))
            {
                await _awardService.NominatePlayerAsync(awardId, playerId);
            }

            return RedirectToAction(nameof(Nominate), new { awardId });
        }

        // ✅ POST: Remove a nomination
        [HttpPost]
        public async Task<IActionResult> RemoveNomination(int awardId, int playerId)
        {
            await _awardService.RemoveNominationAsync(awardId, playerId);
            return RedirectToAction(nameof(Nominate), new { awardId });
        }
    }
}
