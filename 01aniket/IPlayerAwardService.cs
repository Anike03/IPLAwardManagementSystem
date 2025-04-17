using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IPlayerAwardService
    {
        Task<IEnumerable<PlayerAwardDto>> GetAllAsync();
        Task<PlayerAwardDto?> GetByIdAsync(int playerId, int awardId);
        Task<PlayerAwardDto> CreateAsync(PlayerAwardCreateDto dto);
        Task<bool> UpdateAsync(int playerId, int awardId, PlayerAwardUpdateDto dto);
        Task<bool> DeleteAsync(int playerId, int awardId);

        Task<IEnumerable<PlayerAwardDto>> GetAwardsByPlayerIdAsync(int playerId);
        Task<IEnumerable<PlayerAwardDto>> GetNominationsByAwardIdAsync(int awardId);
        Task<bool> IsPlayerNominatedAsync(int playerId, int awardId);

        Task<IEnumerable<PlayerAwardDto>> GetWinnersAsync();
        Task<IEnumerable<PlayerAwardDto>> GetResultsAsync();
    }
}
