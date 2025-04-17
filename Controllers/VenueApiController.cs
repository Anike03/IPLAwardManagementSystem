using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueApiController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenueApiController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var venues = await _venueService.GetAllVenuesAsync();
            return Ok(venues);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            if (venue == null) return NotFound();
            return Ok(venue);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VenueCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _venueService.CreateVenueAsync(dto);
            return Ok(new { message = "Venue created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VenueUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _venueService.UpdateVenueAsync(id, dto);
            return Ok(new { message = "Venue updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _venueService.DeleteVenueAsync(id);
            return Ok(new { message = "Venue deleted successfully." });
        }
    }
}
