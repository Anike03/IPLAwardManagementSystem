using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueTeamApiController : ControllerBase
    {
        private readonly IVenueTeamService _service;

        public VenueTeamApiController(IVenueTeamService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{venueId}/{teamId}")]
        public async Task<IActionResult> Get(int venueId, int teamId)
        {
            var item = await _service.GetByIdAsync(venueId, teamId);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VenueTeamCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.CreateAsync(dto);
            return Ok(new { message = "Venue-Team link created successfully." });
        }

        [HttpDelete("{venueId}/{teamId}")]
        public async Task<IActionResult> Delete(int venueId, int teamId)
        {
            await _service.DeleteAsync(venueId, teamId);
            return Ok(new { message = "Venue-Team link deleted successfully." });
        }
    }
}
