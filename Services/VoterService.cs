// Services/VoterService.cs
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
    public class VoterService : IVoterService
    {
        private readonly ApplicationDbContext _context;

        public VoterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VoterDto> CreateVoterAsync(VoterCreateDto voterCreateDto)
        {
            var voter = new Voter
            {
                Name = voterCreateDto.Name,
                Email = voterCreateDto.Email,
                VoterId = voterCreateDto.VoterId ?? Guid.NewGuid().ToString(),
                Role = voterCreateDto.Role,
                JoinedDate = DateTime.UtcNow,
                IsVerified = false,
                IsActive = true
            };

            _context.Voters.Add(voter);
            await _context.SaveChangesAsync();

            return MapToVoterDto(voter);
        }

        public async Task<IEnumerable<VoterDto>> GetAllVotersAsync(bool? isActive = null)
        {
            IQueryable<Voter> query = _context.Voters;

            if (isActive.HasValue)
            {
                query = query.Where(v => v.IsActive == isActive.Value);
            }

            var voters = await query.ToListAsync();
            return voters.Select(MapToVoterDto);
        }

        public async Task<VoterDto> GetVoterByIdAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) return null;

            return MapToVoterDto(voter);
        }

        public async Task UpdateVoterAsync(int id, VoterUpdateDto voterUpdateDto)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new Exception("Voter not found");

            if (!string.IsNullOrEmpty(voterUpdateDto.Name))
            {
                voter.Name = voterUpdateDto.Name;
            }

            if (!string.IsNullOrEmpty(voterUpdateDto.Email))
            {
                voter.Email = voterUpdateDto.Email;
            }

            if (!string.IsNullOrEmpty(voterUpdateDto.Role))
            {
                voter.Role = voterUpdateDto.Role;
            }

            if (voterUpdateDto.IsVerified.HasValue)
            {
                voter.IsVerified = voterUpdateDto.IsVerified.Value;
            }

            if (voterUpdateDto.IsActive.HasValue)
            {
                voter.IsActive = voterUpdateDto.IsActive.Value;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteVoterAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new Exception("Voter not found");

            _context.Voters.Remove(voter);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleVoterStatusAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new Exception("Voter not found");

            voter.IsActive = !voter.IsActive;
            await _context.SaveChangesAsync();
        }

        public async Task VerifyVoterAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new Exception("Voter not found");

            voter.IsVerified = true;
            await _context.SaveChangesAsync();
        }

        private static VoterDto MapToVoterDto(Voter voter)
        {
            return new VoterDto
            {
                Id = voter.Id,
                Name = voter.Name,
                Email = voter.Email,
                VoterId = voter.VoterId,
                Role = voter.Role,
                JoinedDate = voter.JoinedDate,
                IsVerified = voter.IsVerified,
                IsActive = voter.IsActive
            };
        }
    }
}