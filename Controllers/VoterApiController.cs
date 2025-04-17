using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterApiController : ControllerBase
    {
        private readonly IVoterService _voterService;

        public VoterApiController(IVoterService voterService)
        {
            _voterService = voterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var voters = await _voterService.GetAllVotersAsync();
            return Ok(voters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var voter = await _voterService.GetVoterByIdAsync(id);
            if (voter == null) return NotFound();
            return Ok(voter);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoterCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _voterService.CreateVoterAsync(dto);
            return Ok(new { message = "Voter created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VoterUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _voterService.UpdateVoterAsync(id, dto);
            return Ok(new { message = "Voter updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _voterService.DeleteVoterAsync(id);
            return Ok(new { message = "Voter deleted successfully." });
        }
    }
}
