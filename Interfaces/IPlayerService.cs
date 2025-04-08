// Interfaces/IPlayerService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerDto> CreatePlayerAsync(PlayerCreateDto playerCreateDto);
        Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
        Task<PlayerDto> GetPlayerByIdAsync(int id);
        Task UpdatePlayerAsync(int id, PlayerUpdateDto playerUpdateDto);
        Task DeletePlayerAsync(int id);
        Task<IEnumerable<AwardDto>> GetPlayerAwardsAsync(int playerId);
        Task<IEnumerable<PlayerDto>> GetPlayersByTeamAsync(int teamId);
    }
}