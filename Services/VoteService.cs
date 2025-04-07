// Services/VoteService.cs
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using IPLAwardManagementSystem.Data;

namespace IPLAwardManagementSystem.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext _context;

        public VoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VoteDto> CastVoteAsync(VoteCreateDto voteCreateDto)
        {
            // Check if voter has already voted for this award
            var existingVote = await _context.Votes
                .FirstOrDefaultAsync(v => v.AwardId == voteCreateDto.AwardId && v.VoterId == voteCreateDto.VoterId);

            if (existingVote != null)
            {
                throw new Exception("You have already voted for this award");
            }

            var vote = new Vote
            {
                AwardId = voteCreateDto.AwardId,
                PlayerId = voteCreateDto.PlayerId,
                VoterId = voteCreateDto.VoterId,
                VoteDate = DateTime.UtcNow
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            return await GetVoteByIdAsync(vote.Id);
        }

        public async Task<IEnumerable<VoteDto>> GetAllVotesAsync()
        {
            var votes = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .ToListAsync();

            return votes.Select(MapToVoteDto);
        }

        public async Task<VoteDto> GetVoteByIdAsync(int id)
        {
            var vote = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vote == null) return null;

            return MapToVoteDto(vote);
        }

        public async Task<IEnumerable<VoteDto>> GetVotesByAwardAsync(int awardId)
        {
            var votes = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .Where(v => v.AwardId == awardId)
                .ToListAsync();

            return votes.Select(MapToVoteDto);
        }

        public async Task<IEnumerable<VoteDto>> GetVotesByPlayerAsync(int playerId)
        {
            var votes = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .Where(v => v.PlayerId == playerId)
                .ToListAsync();

            return votes.Select(MapToVoteDto);
        }

        public async Task<IEnumerable<VoteDto>> GetVotesByVoterAsync(int voterId)
        {
            var votes = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .Where(v => v.VoterId == voterId)
                .ToListAsync();

            return votes.Select(MapToVoteDto);
        }

        public async Task DeleteVoteAsync(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null) throw new Exception("Vote not found");

            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
        }

        private static VoteDto MapToVoteDto(Vote vote)
        {
            return new VoteDto
            {
                Id = vote.Id,
                AwardId = vote.AwardId,
                PlayerId = vote.PlayerId,
                VoterId = vote.VoterId,
                VoteDate = vote.VoteDate,
                AwardName = vote.Award?.Name,
                PlayerName = vote.Player?.Name,
                VoterName = vote.Voter?.Name
            };
        }
    }
}