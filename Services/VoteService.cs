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
    }
}