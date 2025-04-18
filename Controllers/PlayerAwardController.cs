using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class PlayerAwardController : Controller
    {
        private readonly IAwardService _awardService;

        public PlayerAwardController(IAwardService awardService)
        {
            _awardService = awardService;
        }

        public async Task<IActionResult> Index()
        {
            var nominations = await _awardService.GetAllAwardNomineesAsync();

            // Optional: Determine winners per award
            var grouped = nominations
                .GroupBy(n => n.AwardId)
                .ToDictionary(g => g.Key, g => g.Max(n => n.VotesReceived));

            foreach (var nominee in nominations)
            {
                var maxVotes = grouped[nominee.AwardId];
                nominee.IsWinner = nominee.VotesReceived == maxVotes && maxVotes > 0;
            }

            return View(nominations); // This points to Views/PlayerAward/Index.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int playerId, int awardId)
        {
            await _awardService.RemoveNominationAsync(awardId, playerId);
            return RedirectToAction(nameof(Index));
        }
    }
}
