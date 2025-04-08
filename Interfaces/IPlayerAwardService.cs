// Interfaces/IPlayerAwardService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IPlayerAwardService
    {
        Task<PlayerAwardDto> NominatePlayerAsync(PlayerAwardCreateDto playerAwardCreateDto);
        Task<IEnumerable<PlayerAwardDto>> GetAllNominationsAsync();
        Task<PlayerAwardDto> GetNominationAsync(int playerId, int awardId);
        Task UpdateNominationAsync(int playerId, int awardId, PlayerAwardUpdateDto playerAwardUpdateDto);
        Task DeleteNominationAsync(int playerId, int awardId);
        Task<IEnumerable<PlayerAwardDto>> GetNominationsByPlayerAsync(int playerId);
        Task<IEnumerable<PlayerAwardDto>> GetNominationsByAwardAsync(int awardId);
        Task<IEnumerable<PlayerAwardDto>> GetWinnersAsync();
    }
}