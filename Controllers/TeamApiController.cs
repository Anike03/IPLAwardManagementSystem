using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamApiController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamApiController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _teamService.CreateTeamAsync(dto);
            return Ok(new { message = "Team created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeamUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _teamService.UpdateTeamAsync(id, dto);
            return Ok(new { message = "Team updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.DeleteTeamAsync(id);
            return Ok(new { message = "Team deleted successfully." });
        }
    }
}
