using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IAwardService
    {
        Task<AwardDto> GetAwardByIdAsync(int id);
        Task<IEnumerable<AwardDto>> GetAllAwardsAsync();
        Task<AwardDto> CreateAwardAsync(AwardCreateDto awardDto);
        Task UpdateAwardAsync(int id, AwardUpdateDto awardDto);
        Task DeleteAwardAsync(int id);

        Task NominatePlayerAsync(int awardId, int playerId);
        Task RemoveNominationAsync(int awardId, int playerId);
        Task DeclareWinnerAsync(int awardId, int playerId);
        Task RevokeWinnerStatusAsync(int awardId, int playerId);

        Task<IEnumerable<PlayerAwardDto>> GetAwardNomineesAsync(int awardId);
        Task<IEnumerable<PlayerAwardDto>> GetAwardWinnersAsync(int awardId);
        Task<int> GetVoteCountAsync(int awardId, int playerId);
        Task<bool> IsPlayerNominatedAsync(int awardId, int playerId);

        Task<IEnumerable<PlayerAwardDto>> GetAllAwardNomineesAsync();
    }
}