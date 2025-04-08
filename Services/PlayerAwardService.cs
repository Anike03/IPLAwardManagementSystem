using AutoMapper;
using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;
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

        public async Task<PlayerAwardDto> NominatePlayerAsync(PlayerAwardCreateDto playerAwardCreateDto)
        {
            // Check if player is already nominated for this award
            var existingNomination = await _context.PlayerAwards
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerAwardCreateDto.PlayerId &&
                                         pa.AwardId == playerAwardCreateDto.AwardId);

            if (existingNomination != null)
            {
                throw new InvalidOperationException("Player is already nominated for this award");
            }

            var playerAward = _mapper.Map<PlayerAward>(playerAwardCreateDto);
            _context.PlayerAwards.Add(playerAward);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlayerAwardDto>(playerAward);
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetAllNominationsAsync()
        {
            var nominations = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlayerAwardDto>>(nominations);
        }

        public async Task<PlayerAwardDto> GetNominationAsync(int playerId, int awardId)
        {
            var nomination = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerId && pa.AwardId == awardId);

            if (nomination == null) throw new KeyNotFoundException("Nomination not found");
            return _mapper.Map<PlayerAwardDto>(nomination);
        }

        public async Task UpdateNominationAsync(int playerId, int awardId, PlayerAwardUpdateDto playerAwardUpdateDto)
        {
            var nomination = await _context.PlayerAwards
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerId && pa.AwardId == awardId);

            if (nomination == null) throw new KeyNotFoundException("Nomination not found");

            _mapper.Map(playerAwardUpdateDto, nomination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNominationAsync(int playerId, int awardId)
        {
            var nomination = await _context.PlayerAwards
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerId && pa.AwardId == awardId);

            if (nomination == null) throw new KeyNotFoundException("Nomination not found");

            _context.PlayerAwards.Remove(nomination);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetNominationsByPlayerAsync(int playerId)
        {
            var nominations = await _context.PlayerAwards
                .Where(pa => pa.PlayerId == playerId)
                .Include(pa => pa.Award)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlayerAwardDto>>(nominations);
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetNominationsByAwardAsync(int awardId)
        {
            var nominations = await _context.PlayerAwards
                .Where(pa => pa.AwardId == awardId)
                .Include(pa => pa.Player)
                .ThenInclude(p => p.Team)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlayerAwardDto>>(nominations);
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetWinnersAsync()
        {
            var winners = await _context.PlayerAwards
                .Where(pa => pa.IsWinner)
                .Include(pa => pa.Player)
                .ThenInclude(p => p.Team)
                .Include(pa => pa.Award)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlayerAwardDto>>(winners);
        }
    }
}