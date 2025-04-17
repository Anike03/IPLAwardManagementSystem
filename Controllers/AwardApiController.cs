using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardApiController : ControllerBase
    {
        private readonly IAwardService _awardService;
        private readonly IPlayerService _playerService;
        private readonly IVoteService _voteService;

        public AwardApiController(
            IAwardService awardService,
            IPlayerService playerService,
            IVoteService voteService)
        {
            _awardService = awardService;
            _playerService = playerService;
            _voteService = voteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAwards()
        {
            var awards = await _awardService.GetAllAwardsAsync();
            return Ok(awards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAward(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null) return NotFound();
            return Ok(award);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAward([FromBody] AwardCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _awardService.CreateAwardAsync(dto);
            return Ok(new { message = "Award created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAward(int id, [FromBody] AwardUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _awardService.UpdateAwardAsync(id, dto);
            return Ok(new { message = "Award updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            await _awardService.DeleteAwardAsync(id);
            return Ok(new { message = "Award deleted successfully." });
        }

        // ✅ GET: Nominees for an award, with calculated votes and winner
        [HttpGet("{awardId}/nominees")]
        public async Task<IActionResult> GetNominees(int awardId)
        {
            var nominees = await _awardService.GetAwardNomineesAsync(awardId);
            var votes = await _voteService.GetAllVotesAsync();

            var voteGroups = votes
                .Where(v => v.AwardId == awardId)
                .GroupBy(v => v.PlayerId)
                .ToDictionary(g => g.Key, g => g.Count());

            var maxVotes = voteGroups.Any() ? voteGroups.Max(x => x.Value) : 0;

            foreach (var nominee in nominees)
            {
                nominee.VotesReceived = voteGroups.ContainsKey(nominee.PlayerId) ? voteGroups[nominee.PlayerId] : 0;
                nominee.IsWinner = nominee.VotesReceived == maxVotes && maxVotes > 0;
            }

            return Ok(nominees);
        }

        // ✅ POST: Nominate a player for an award
        [HttpPost("{awardId}/nominate/{playerId}")]
        public async Task<IActionResult> NominatePlayer(int awardId, int playerId)
        {
            if (!await _awardService.IsPlayerNominatedAsync(awardId, playerId))
            {
                await _awardService.NominatePlayerAsync(awardId, playerId);
            }
            return Ok(new { message = "Player nominated." });
        }

        // ✅ DELETE: Remove nomination
        [HttpDelete("{awardId}/nominate/{playerId}")]
        public async Task<IActionResult> RemoveNomination(int awardId, int playerId)
        {
            await _awardService.RemoveNominationAsync(awardId, playerId);
            return Ok(new { message = "Nomination removed." });
        }
    }
}
