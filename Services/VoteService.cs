using AutoMapper;
using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
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

        public async Task<IEnumerable<VoteDto>> GetAllVotesAsync()
        {
            var votes = await _context.Votes
                .Include(v => v.Player)
                .Include(v => v.Award)
                .Include(v => v.Voter)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VoteDto>>(votes);
        }

        public async Task<VoteDto?> GetVoteByIdAsync(int id)
        {
            var vote = await _context.Votes
                .Include(v => v.Player)
                .Include(v => v.Award)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(v => v.Id == id);

            return vote == null ? null : _mapper.Map<VoteDto>(vote);
        }

        public async Task<VoteDto> CreateVoteAsync(VoteCreateDto dto)
        {
            var vote = _mapper.Map<Vote>(dto);
            vote.VoteDate = DateTime.UtcNow;

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            return _mapper.Map<VoteDto>(vote);
        }

        public async Task UpdateVoteAsync(int id, VoteUpdateDto dto)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null) throw new Exception("Vote not found");

            _mapper.Map(dto, vote);
            await _context.SaveChangesAsync();
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

        // ✅ Results with Winner/Nominated logic
        public async Task<IEnumerable<VoteResultDto>> GetVoteResultsAsync()
        {
            var groupedVotes = await _context.Votes
                .Include(v => v.Player)
                .Include(v => v.Award)
                .GroupBy(v => new { v.AwardId, v.PlayerId })
                .Select(g => new VoteResultDto
                {
                    AwardId = g.Key.AwardId,
                    PlayerId = g.Key.PlayerId,
                    AwardName = g.First().Award != null ? g.First().Award.Name : "Unknown",
                    PlayerName = g.First().Player != null ? g.First().Player.Name : "Unknown",
                    TotalVotes = g.Count()
                })
                .ToListAsync();

            // determine winners for each award
            var winnersByAward = groupedVotes
                .GroupBy(r => r.AwardId)
                .Select(g => g.OrderByDescending(x => x.TotalVotes).First().PlayerId)
                .ToHashSet();

            foreach (var result in groupedVotes)
            {
                result.Status = winnersByAward.Contains(result.PlayerId) ? "Winner" : "Nominated";
            }

            return groupedVotes;
        }
    }
}
