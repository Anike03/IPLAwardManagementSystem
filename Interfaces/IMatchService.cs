// Interfaces/IMatchService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IMatchService
    {
        Task<MatchDto> CreateMatchAsync(MatchCreateDto matchCreateDto);
        Task<IEnumerable<MatchDto>> GetAllMatchesAsync();
        Task<MatchDto> GetMatchByIdAsync(int id);
        Task UpdateMatchAsync(int id, MatchUpdateDto matchUpdateDto);
        Task DeleteMatchAsync(int id);
        Task<IEnumerable<MatchDto>> GetMatchesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}