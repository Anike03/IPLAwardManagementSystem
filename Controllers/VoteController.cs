using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IPLAwardManagementSystem.Controllers
{
    public class VoteController : Controller
    {
        private readonly IVoteService _voteService;
        private readonly IPlayerService _playerService;
        private readonly IAwardService _awardService;
        private readonly IVoterService _voterService;

        public VoteController(
            IVoteService voteService,
            IPlayerService playerService,
            IAwardService awardService,
            IVoterService voterService)
        {
            _voteService = voteService;
            _playerService = playerService;
            _awardService = awardService;
            _voterService = voterService;
        }

        public async Task<IActionResult> Index()
        {
            var votes = await _voteService.GetAllVotesAsync();
            return View(votes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vote = await _voteService.GetVoteByIdAsync(id);
            if (vote == null) return NotFound();
            return View(vote);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoteCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _voteService.CreateVoteAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdownData();
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vote = await _voteService.GetVoteByIdAsync(id);
            if (vote == null) return NotFound();

            var dto = new VoteUpdateDto
            {
                Id = vote.Id,
                PlayerId = vote.PlayerId,
                AwardId = vote.AwardId,
                VoterId = vote.VoterId,
                VoteDate = vote.VoteDate
            };

            await LoadDropdownData();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VoteUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _voteService.UpdateVoteAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdownData();
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vote = await _voteService.GetVoteByIdAsync(id);
            if (vote == null) return NotFound();
            return View(vote);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _voteService.DeleteVoteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ✅ Results Page: Show vote totals grouped by player and award
        public async Task<IActionResult> Results()
        {
            var results = await _voteService.GetVoteResultsAsync();
            return View(results);
        }

        private async Task LoadDropdownData()
        {
            var players = await _playerService.GetAllPlayersAsync();
            var awards = await _awardService.GetAllAwardsAsync();
            var voters = await _voterService.GetAllVotersAsync();

            ViewBag.Players = new SelectList(players, "PlayerId", "Name");
            ViewBag.Awards = new SelectList(awards, "AwardId", "Name");
            ViewBag.Voters = new SelectList(voters, "VoterId", "Name");
        }
    }
}
