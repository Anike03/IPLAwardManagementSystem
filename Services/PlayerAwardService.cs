// Services/PlayerAwardService.cs
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
    public class PlayerAwardService : IPlayerAwardService
    {
        private readonly ApplicationDbContext _context;

        public PlayerAwardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlayerAwardDto> NominatePlayerAsync(PlayerAwardCreateDto nominationDto)
        {
            // Check if nomination already exists
            var existingNomination = await _context.PlayerAwards
                .FirstOrDefaultAsync(pa => pa.PlayerId == nominationDto.PlayerId && pa.AwardId == nominationDto.AwardId);

            if (existingNomination != null)
            {
                throw new Exception("This player is already nominated for this award");
            }

            var nomination = new PlayerAward
            {
                PlayerId = nominationDto.PlayerId,
                AwardId = nominationDto.AwardId,
                IsWinner = nominationDto.IsWinner,
                NominationDate = DateTime.UtcNow
            };

            _context.PlayerAwards.Add(nomination);
            await _context.SaveChangesAsync();

            return await GetNominationAsync(nomination.PlayerId, nomination.AwardId);
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetAllNominationsAsync()
        {
            var nominations = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .ToListAsync();

            return nominations.Select(MapToPlayerAwardDto);
        }

        public async Task<PlayerAwardDto> GetNominationAsync(int playerId, int awardId)
        {
            var nomination = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerId && pa.AwardId == awardId);

            if (nomination == null) return null;

            return MapToPlayerAwardDto(nomination);
        }

        public async Task UpdateNominationAsync(int playerId, int awardId, PlayerAwardUpdateDto updateDto)
        {
            var nomination = await _context.PlayerAwards
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerId && pa.AwardId == awardId);

            if (nomination == null) throw new Exception("Nomination not found");

            if (updateDto.IsWinner.HasValue)
            {
                nomination.IsWinner = updateDto.IsWinner.Value;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveNominationAsync(int playerId, int awardId)
        {
            var nomination = await _context.PlayerAwards
                .FirstOrDefaultAsync(pa => pa.PlayerId == playerId && pa.AwardId == awardId);

            if (nomination == null) throw new Exception("Nomination not found");

            _context.PlayerAwards.Remove(nomination);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetNominationsByPlayerAsync(int playerId)
        {
            var nominations = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .Where(pa => pa.PlayerId == playerId)
                .ToListAsync();

            return nominations.Select(MapToPlayerAwardDto);
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetNominationsByAwardAsync(int awardId)
        {
            var nominations = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .Where(pa => pa.AwardId == awardId)
                .ToListAsync();

            return nominations.Select(MapToPlayerAwardDto);
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetWinnersAsync()
        {
            var nominations = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .Where(pa => pa.IsWinner)
                .ToListAsync();

            return nominations.Select(MapToPlayerAwardDto);
        }

        private static PlayerAwardDto MapToPlayerAwardDto(PlayerAward nomination)
        {
            return new PlayerAwardDto
            {
                PlayerId = nomination.PlayerId,
                AwardId = nomination.AwardId,
                IsWinner = nomination.IsWinner,
                NominationDate = nomination.NominationDate,
                PlayerName = nomination.Player?.Name,
                AwardName = nomination.Award?.Name
            };
        }
    }
}