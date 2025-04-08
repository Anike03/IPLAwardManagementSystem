using AutoMapper;
using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;
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

        public async Task<VoterDto> CreateVoterAsync(VoterCreateDto voterCreateDto)
        {
            var voter = _mapper.Map<Voter>(voterCreateDto);
            _context.Voters.Add(voter);
            await _context.SaveChangesAsync();
            return _mapper.Map<VoterDto>(voter);
        }

        public async Task<IEnumerable<VoterDto>> GetAllVotersAsync()
        {
            var voters = await _context.Voters.ToListAsync();
            return _mapper.Map<IEnumerable<VoterDto>>(voters);
        }

        public async Task<VoterDto> GetVoterByIdAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new KeyNotFoundException("Voter not found");
            return _mapper.Map<VoterDto>(voter);
        }

        public async Task UpdateVoterAsync(int id, VoterUpdateDto voterUpdateDto)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new KeyNotFoundException("Voter not found");

            _mapper.Map(voterUpdateDto, voter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVoterAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new KeyNotFoundException("Voter not found");

            _context.Voters.Remove(voter);
            await _context.SaveChangesAsync();
        }

        public async Task VerifyVoterAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new KeyNotFoundException("Voter not found");

            voter.IsVerified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VoteDto>> GetVotesByVoterAsync(int voterId)
        {
            var votes = await _context.Votes
                .Where(v => v.VoterId == voterId)
                .Include(v => v.Award)
                .Include(v => v.Player)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VoteDto>>(votes);
        }
    }
}