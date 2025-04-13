using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;

        public PlayerController(IPlayerService playerService, ITeamService teamService)
        {
            _playerService = playerService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetAllPlayersAsync();
            return View(players);
        }

        public async Task<IActionResult> Details(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
                return NotFound();

            return View(player);
        }

        public async Task<IActionResult> Create()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _playerService.CreatePlayerAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            var teams = await _teamService.GetAllTeamsAsync();
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName");
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
                return NotFound();

            var editDto = new PlayerUpdateDto
            {
                PlayerId = player.PlayerId,
                Name = player.Name,
                Role = player.Role,
                TeamId = player.TeamId,
                Age = player.Age
            };

            var teams = await _teamService.GetAllTeamsAsync();
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName", editDto.TeamId);

            return View(editDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlayerUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _playerService.UpdatePlayerAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }

            var teams = await _teamService.GetAllTeamsAsync();
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName", dto.TeamId);

            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
                return NotFound();

            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _playerService.DeletePlayerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}