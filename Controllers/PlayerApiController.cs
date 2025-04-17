using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerApiController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;

        public PlayerApiController(IPlayerService playerService, ITeamService teamService)
        {
            _playerService = playerService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var players = await _playerService.GetAllPlayersAsync();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null) return NotFound();
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlayerCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _playerService.CreatePlayerAsync(dto);
            return Ok(new { message = "Player created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlayerUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _playerService.UpdatePlayerAsync(id, dto);
            return Ok(new { message = "Player updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _playerService.DeletePlayerAsync(id);
            return Ok(new { message = "Player deleted successfully." });
        }

        // Optional: Fetch all teams for form dropdowns
        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(new { teams });
        }
    }
}
