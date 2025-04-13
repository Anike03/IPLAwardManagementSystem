using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace IPLAwardManagementSystem.Services
{
    public class VoterService : IVoterService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VoterService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VoterDto> CreateVoterAsync(VoterCreateDto dto)
        {
            var voter = _mapper.Map<Voter>(dto);
            _context.Voters.Add(voter);
            await _context.SaveChangesAsync();
            return _mapper.Map<VoterDto>(voter);
        }

        public async Task DeleteVoterAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter != null)
            {
                _context.Voters.Remove(voter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<VoterDto>> GetAllVotersAsync()
        {
            var voters = await _context.Voters.ToListAsync();
            return _mapper.Map<List<VoterDto>>(voters);
        }

        public async Task<VoterDto?> GetVoterByIdAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            return voter == null ? null : _mapper.Map<VoterDto>(voter);
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

        public async Task UpdateVoterAsync(int id, VoterUpdateDto dto)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter != null)
            {
                _mapper.Map(dto, voter);
                await _context.SaveChangesAsync();
            }
        }
    }
}