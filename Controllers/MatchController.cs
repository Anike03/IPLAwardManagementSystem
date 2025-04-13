using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly IVenueService _venueService;
        private readonly ITeamService _teamService;

        public MatchController(IMatchService matchService, IVenueService venueService, ITeamService teamService)
        {
            _matchService = matchService;
            _venueService = venueService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return View(matches);
        }

        public async Task<IActionResult> Details(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null) return NotFound();
            return View(match);
        }

        public async Task<IActionResult> Create()
        {
            var venues = await _venueService.GetAllVenuesAsync();
            var teams = await _teamService.GetAllTeamsAsync();

            ViewBag.Venues = new SelectList(venues, "VenueId", "Name");
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MatchCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _matchService.CreateMatchAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            var venues = await _venueService.GetAllVenuesAsync();
            var teams = await _teamService.GetAllTeamsAsync();

            ViewBag.Venues = new SelectList(venues, "VenueId", "Name");
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName");

            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null) return NotFound();

            var editDto = new MatchUpdateDto
            {
                MatchDate = match.MatchDate,
                VenueId = match.VenueId,
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId
            };

            var venues = await _venueService.GetAllVenuesAsync();
            var teams = await _teamService.GetAllTeamsAsync();

            ViewBag.Venues = new SelectList(venues, "VenueId", "Name", editDto.VenueId);
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName");

            return View(editDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MatchUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _matchService.UpdateMatchAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }

            var venues = await _venueService.GetAllVenuesAsync();
            var teams = await _teamService.GetAllTeamsAsync();

            ViewBag.Venues = new SelectList(venues, "VenueId", "Name", dto.VenueId);
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName");

            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null) return NotFound();
            return View(match);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _matchService.DeleteMatchAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}