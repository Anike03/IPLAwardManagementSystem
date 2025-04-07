// Interfaces/IPlayerAwardService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IPlayerAwardService
    {
        Task<PlayerAwardDto> NominatePlayerAsync(PlayerAwardCreateDto nominationDto);
        Task<IEnumerable<PlayerAwardDto>> GetAllNominationsAsync();
        Task<PlayerAwardDto> GetNominationAsync(int playerId, int awardId);
        Task UpdateNominationAsync(int playerId, int awardId, PlayerAwardUpdateDto updateDto);
        Task RemoveNominationAsync(int playerId, int awardId);
        Task<IEnumerable<PlayerAwardDto>> GetNominationsByPlayerAsync(int playerId);
        Task<IEnumerable<PlayerAwardDto>> GetNominationsByAwardAsync(int awardId);
        Task<IEnumerable<PlayerAwardDto>> GetWinnersAsync();
    }
}