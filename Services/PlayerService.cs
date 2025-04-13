using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PlayerDetailDto> CreatePlayerAsync(PlayerCreateDto dto)
        {
            var player = _mapper.Map<Player>(dto);
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlayerDetailDto>(player);
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PlayerListDto>> GetAllPlayersAsync()
        {
            var players = await _context.Players
                .Include(p => p.Team) // Ensure Team data is loaded
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlayerListDto>>(players);
        }


        public async Task<PlayerDetailDto?> GetPlayerByIdAsync(int id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .Include(p => p.PlayerAwards).ThenInclude(pa => pa.Award)
                .FirstOrDefaultAsync(p => p.PlayerId == id);

            return player == null ? null : _mapper.Map<PlayerDetailDto>(player);
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetPlayerAwardsAsync(int playerId)
        {
            var awards = await _context.PlayerAwards
                .Include(pa => pa.Award)
                .Where(pa => pa.PlayerId == playerId)
                .ToListAsync();

            return _mapper.Map<List<PlayerAwardDto>>(awards);
        }

        public async Task UpdatePlayerAsync(int id, PlayerUpdateDto dto)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _mapper.Map(dto, player);
                await _context.SaveChangesAsync();
            }
        }
    }
}