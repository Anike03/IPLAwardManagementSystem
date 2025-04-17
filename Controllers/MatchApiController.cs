using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchApiController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IVenueService _venueService;
        private readonly ITeamService _teamService;

        public MatchApiController(
            IMatchService matchService,
            IVenueService venueService,
            ITeamService teamService)
        {
            _matchService = matchService;
            _venueService = venueService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return Ok(matches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null) return NotFound();
            return Ok(match);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _matchService.CreateMatchAsync(dto);
            return Ok(new { message = "Match created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MatchUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _matchService.UpdateMatchAsync(id, dto);
            return Ok(new { message = "Match updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _matchService.DeleteMatchAsync(id);
            return Ok(new { message = "Match deleted successfully." });
        }

        // Optional: Get dropdown data (venues and teams) if you need them in a frontend form
        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData()
        {
            var venues = await _venueService.GetAllVenuesAsync();
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(new { venues, teams });
        }
    }
}
