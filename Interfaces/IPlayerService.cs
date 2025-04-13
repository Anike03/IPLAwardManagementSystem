using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerListDto>> GetAllPlayersAsync();
        Task<PlayerDetailDto?> GetPlayerByIdAsync(int id);
        Task<PlayerDetailDto> CreatePlayerAsync(PlayerCreateDto dto);
        Task UpdatePlayerAsync(int id, PlayerUpdateDto dto);
        Task DeletePlayerAsync(int id);
        Task<IEnumerable<PlayerAwardDto>> GetPlayerAwardsAsync(int playerId);
    }
}