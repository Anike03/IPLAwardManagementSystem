using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteApiController : ControllerBase
    {
        private readonly IVoteService _voteService;
        private readonly IPlayerService _playerService;
        private readonly IAwardService _awardService;
        private readonly IVoterService _voterService;

        public VoteApiController(
            IVoteService voteService,
            IPlayerService playerService,
            IAwardService awardService,
            IVoterService voterService)
        {
            _voteService = voteService;
            _playerService = playerService;
            _awardService = awardService;
            _voterService = voterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var votes = await _voteService.GetAllVotesAsync();
            return Ok(votes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vote = await _voteService.GetVoteByIdAsync(id);
            if (vote == null) return NotFound();
            return Ok(vote);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoteCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _voteService.CreateVoteAsync(dto);
            return Ok(new { message = "Vote recorded successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VoteUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _voteService.UpdateVoteAsync(id, dto);
            return Ok(new { message = "Vote updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _voteService.DeleteVoteAsync(id);
            return Ok(new { message = "Vote deleted successfully." });
        }

        [HttpGet("results")]
        public async Task<IActionResult> GetResults()
        {
            var results = await _voteService.GetVoteResultsAsync();
            return Ok(results);
        }

        // Optional: Get dropdown options for form
        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData()
        {
            var players = await _playerService.GetAllPlayersAsync();
            var awards = await _awardService.GetAllAwardsAsync();
            var voters = await _voterService.GetAllVotersAsync();

            return Ok(new
            {
                players,
                awards,
                voters
            });
        }
    }
}
