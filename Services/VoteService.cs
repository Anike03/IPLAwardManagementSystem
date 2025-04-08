using AutoMapper;
using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace IPLAwardManagementSystem.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VoteService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VoteDto> CreateVoteAsync(VoteCreateDto voteCreateDto)
        {
            // Check if voter has already voted for this award
            var existingVote = await _context.Votes
                .FirstOrDefaultAsync(v => v.VoterId == voteCreateDto.VoterId &&
                                         v.AwardId == voteCreateDto.AwardId);

            if (existingVote != null)
            {
                throw new InvalidOperationException("Voter has already voted for this award");
            }

            var vote = _mapper.Map<Vote>(voteCreateDto);
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return _mapper.Map<VoteDto>(vote);
        }

        public async Task<IEnumerable<VoteDto>> GetAllVotesAsync()
        {
            var votes = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VoteDto>>(votes);
        }

        public async Task<VoteDto> GetVoteByIdAsync(int id)
        {
            var vote = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vote == null) throw new KeyNotFoundException("Vote not found");
            return _mapper.Map<VoteDto>(vote);
        }

        public async Task DeleteVoteAsync(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null) throw new KeyNotFoundException("Vote not found");

            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VoteDto>> GetVotesForAwardAsync(int awardId)
        {
            var votes = await _context.Votes
                .Where(v => v.AwardId == awardId)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VoteDto>>(votes);
        }

        public async Task<IEnumerable<VoteDto>> GetVotesForPlayerAsync(int playerId)
        {
            var votes = await _context.Votes
                .Where(v => v.PlayerId == playerId)
                .Include(v => v.Award)
                .Include(v => v.Voter)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VoteDto>>(votes);
        }
    }
}