using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return View(teams);
        }

        public async Task<IActionResult> Details(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            return View(team);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TeamCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _teamService.CreateTeamAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            return View(new TeamUpdateDto { TeamName = team.TeamName, Coach = team.Coach, HomeCity = team.HomeCity, FoundingDate = team.FoundingDate });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeamUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _teamService.UpdateTeamAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _teamService.DeleteTeamAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}