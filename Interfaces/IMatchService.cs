using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IMatchService
    {
        Task<MatchDto?> GetMatchByIdAsync(int id);
        Task<IEnumerable<MatchDto>> GetAllMatchesAsync();
        Task<MatchDto> CreateMatchAsync(MatchCreateDto dto);
        Task UpdateMatchAsync(int id, MatchUpdateDto dto);
        Task DeleteMatchAsync(int id);

        Task<IEnumerable<MatchDto>> GetMatchesByVenueAsync(int venueId);
        Task<IEnumerable<MatchDto>> GetMatchesByTeamAsync(int teamId);
    }
}