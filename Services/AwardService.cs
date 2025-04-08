// Services/AwardService.cs
using AutoMapper;
using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace IPLAwardManagementSystem.Services
{
    public class AwardService : IAwardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AwardService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AwardDto> CreateAwardAsync(AwardCreateDto awardCreateDto)
        {
            var award = _mapper.Map<Award>(awardCreateDto);
            _context.Awards.Add(award);
            await _context.SaveChangesAsync();
            return _mapper.Map<AwardDto>(award);
        }

        public async Task<IEnumerable<AwardDto>> GetAllAwardsAsync(bool? isActive = null)
        {
            IQueryable<Award> query = _context.Awards;

            if (isActive.HasValue)
            {
                query = query.Where(a => a.IsActive == isActive.Value);
            }

            var awards = await query.ToListAsync();
            return _mapper.Map<IEnumerable<AwardDto>>(awards);
        }

        public async Task<AwardDto> GetAwardByIdAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) throw new KeyNotFoundException("Award not found");
            return _mapper.Map<AwardDto>(award);
        }

        public async Task UpdateAwardAsync(int id, AwardUpdateDto awardUpdateDto)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) throw new KeyNotFoundException("Award not found");

            _mapper.Map(awardUpdateDto, award);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAwardAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) throw new KeyNotFoundException("Award not found");

            _context.Awards.Remove(award);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleAwardStatusAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) throw new KeyNotFoundException("Award not found");

            award.IsActive = !award.IsActive;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayersForAwardAsync(int awardId)
        {
            var players = await _context.PlayerAwards
                .Where(pa => pa.AwardId == awardId)
                .Include(pa => pa.Player)
                .ThenInclude(p => p.Team)
                .Select(pa => pa.Player)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }
    }
}
