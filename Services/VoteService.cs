using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;
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

        public async Task<VoteDto> CreateVoteAsync(VoteCreateDto dto)
        {
            var vote = _mapper.Map<Vote>(dto);
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return _mapper.Map<VoteDto>(vote);
        }

        public async Task DeleteVoteAsync(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote != null)
            {
                _context.Votes.Remove(vote);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<VoteDto>> GetAllVotesAsync()
        {
            var votes = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .ToListAsync();

            return _mapper.Map<List<VoteDto>>(votes);
        }

        public async Task<VoteDto?> GetVoteByIdAsync(int id)
        {
            var vote = await _context.Votes
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(v => v.Id == id);

            return vote == null ? null : _mapper.Map<VoteDto>(vote);
        }

        public async Task<IEnumerable<VoteDto>> GetVotesByAwardAsync(int awardId)
        {
            var votes = await _context.Votes
                .Where(v => v.AwardId == awardId)
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .ToListAsync();

            return _mapper.Map<List<VoteDto>>(votes);
        }

        public async Task<IEnumerable<VoteDto>> GetVotesByPlayerAsync(int playerId)
        {
            var votes = await _context.Votes
                .Where(v => v.PlayerId == playerId)
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .ToListAsync();

            return _mapper.Map<List<VoteDto>>(votes);
        }

        public async Task<IEnumerable<VoteDto>> GetVotesByVoterAsync(int voterId)
        {
            var votes = await _context.Votes
                .Where(v => v.VoterId == voterId)
                .Include(v => v.Award)
                .Include(v => v.Player)
                .Include(v => v.Voter)
                .ToListAsync();

            return _mapper.Map<List<VoteDto>>(votes);
        }

        public async Task<int> GetTotalVotesForPlayerAsync(int awardId, int playerId)
        {
            return await _context.Votes.CountAsync(v => v.AwardId == awardId && v.PlayerId == playerId);
        }
    }
}