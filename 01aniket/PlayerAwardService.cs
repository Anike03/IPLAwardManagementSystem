using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace IPLAwardManagementSystem.Services
{
    public class PlayerAwardService : IPlayerAwardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlayerAwardService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayerAwardDto> CreateAsync(PlayerAwardCreateDto dto)
        {
            var entity = _mapper.Map<PlayerAward>(dto);
            _context.PlayerAwards.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlayerAwardDto>(entity);
        }

        public async Task<bool> DeleteAsync(int playerId, int awardId)
        {
            var entity = await _context.PlayerAwards.FindAsync(playerId, awardId);
            if (entity != null)
            {
                _context.PlayerAwards.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetAllAsync()
        {
            var list = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .ToListAsync();
            return _mapper.Map<List<PlayerAwardDto>>(list);
        }

        public async Task<PlayerAwardDto?> GetByIdAsync(int playerId, int awardId)
        {
            var entity = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerId && pa.AwardId == awardId);
            return entity == null ? null : _mapper.Map<PlayerAwardDto>(entity);
        }

        public async Task<bool> UpdateAsync(int playerId, int awardId, PlayerAwardUpdateDto dto)
        {
            var entity = await _context.PlayerAwards.FindAsync(playerId, awardId);
            if (entity != null)
            {
                _mapper.Map(dto, entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetAwardsByPlayerIdAsync(int playerId)
        {
            var list = await _context.PlayerAwards
                .Include(pa => pa.Award)
                .Include(pa => pa.Player)
                .Where(pa => pa.PlayerId == playerId)
                .ToListAsync();
            return _mapper.Map<List<PlayerAwardDto>>(list);
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetNominationsByAwardIdAsync(int awardId)
        {
            var list = await _context.PlayerAwards
                .Include(pa => pa.Award)
                .Include(pa => pa.Player)
                .Where(pa => pa.AwardId == awardId)
                .ToListAsync();
            return _mapper.Map<List<PlayerAwardDto>>(list);
        }

        public async Task<bool> IsPlayerNominatedAsync(int playerId, int awardId)
        {
            return await _context.PlayerAwards.AnyAsync(pa => pa.PlayerId == playerId && pa.AwardId == awardId);
        }

        // ? New: Winners List
        public async Task<IEnumerable<PlayerAwardDto>> GetWinnersAsync()
        {
            var list = await _context.PlayerAwards
                .Where(pa => pa.IsWinner)
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .ToListAsync();
            return _mapper.Map<List<PlayerAwardDto>>(list);
        }

        // ? New: Results (all nominations, grouped or counted)
        public async Task<IEnumerable<PlayerAwardDto>> GetResultsAsync()
        {
            var results = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .ToListAsync();
            return _mapper.Map<List<PlayerAwardDto>>(results);
        }
    }
}
