// Interfaces/IVoteService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IVoteService
    {
        Task<VoteDto> CreateVoteAsync(VoteCreateDto voteCreateDto);
        Task<IEnumerable<VoteDto>> GetAllVotesAsync();
        Task<VoteDto> GetVoteByIdAsync(int id);
        Task DeleteVoteAsync(int id);
        Task<IEnumerable<VoteDto>> GetVotesForAwardAsync(int awardId);
        Task<IEnumerable<VoteDto>> GetVotesForPlayerAsync(int playerId);
    }
}