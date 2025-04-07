// Services/AwardService.cs
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
    public class AwardService : IAwardService
    {
        private readonly ApplicationDbContext _context;

        public AwardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AwardDto> CreateAwardAsync(AwardCreateDto awardCreateDto)
        {
            var award = new Award
            {
                Name = awardCreateDto.Name,
                Description = awardCreateDto.Description,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.Awards.Add(award);
            await _context.SaveChangesAsync();

            return MapToAwardDto(award);
        }

        public async Task<IEnumerable<AwardDto>> GetAllAwardsAsync(bool? isActive = null)
        {
            IQueryable<Award> query = _context.Awards;

            if (isActive.HasValue)
            {
                query = query.Where(a => a.IsActive == isActive.Value);
            }

            var awards = await query.ToListAsync();
            return awards.Select(MapToAwardDto);
        }

        public async Task<AwardDto> GetAwardByIdAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) return null;

            return MapToAwardDto(award);
        }

        public async Task UpdateAwardAsync(int id, AwardUpdateDto awardUpdateDto)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) throw new Exception("Award not found");

            if (!string.IsNullOrEmpty(awardUpdateDto.Name))
            {
                award.Name = awardUpdateDto.Name;
            }

            if (awardUpdateDto.Description != null)
            {
                award.Description = awardUpdateDto.Description;
            }

            if (awardUpdateDto.IsActive.HasValue)
            {
                award.IsActive = awardUpdateDto.IsActive.Value;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAwardAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) throw new Exception("Award not found");

            _context.Awards.Remove(award);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleAwardStatusAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) throw new Exception("Award not found");

            award.IsActive = !award.IsActive;
            await _context.SaveChangesAsync();
        }

        private static AwardDto MapToAwardDto(Award award)
        {
            return new AwardDto
            {
                Id = award.Id,
                Name = award.Name,
                Description = award.Description,
                CreatedDate = award.CreatedDate,
                IsActive = award.IsActive
            };
        }
    }
}