using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace IPLAwardManagementSystem.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlayerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayerDto> CreatePlayerAsync(PlayerCreateDto playerCreateDto)
        {
            var player = _mapper.Map<Player>(playerCreateDto);
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlayerDto>(player);
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            var players = await _context.Players.Include(p => p.Team).ToListAsync();
            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .Include(p => p.PlayerAwards)
                .ThenInclude(pa => pa.Award)
                .FirstOrDefaultAsync(p => p.PlayerId == id);

            if (player == null) throw new KeyNotFoundException("Player not found");
            return _mapper.Map<PlayerDto>(player);
        }

        public async Task UpdatePlayerAsync(int id, PlayerUpdateDto playerUpdateDto)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) throw new KeyNotFoundException("Player not found");

            _mapper.Map(playerUpdateDto, player);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) throw new KeyNotFoundException("Player not found");

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AwardDto>> GetPlayerAwardsAsync(int playerId)
        {
            var awards = await _context.PlayerAwards
                .Where(pa => pa.PlayerId == playerId)
                .Include(pa => pa.Award)
                .Select(pa => pa.Award)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AwardDto>>(awards);
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayersByTeamAsync(int teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .Include(p => p.Team)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }
    }
}