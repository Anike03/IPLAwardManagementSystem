// Interfaces/IVoteService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IVoteService
    {
        Task<VoteDto> CastVoteAsync(VoteCreateDto voteCreateDto);
        Task<IEnumerable<VoteDto>> GetAllVotesAsync();
        Task<VoteDto> GetVoteByIdAsync(int id);
        Task<IEnumerable<VoteDto>> GetVotesByAwardAsync(int awardId);
        Task<IEnumerable<VoteDto>> GetVotesByPlayerAsync(int playerId);
        Task<IEnumerable<VoteDto>> GetVotesByVoterAsync(int voterId);
        Task DeleteVoteAsync(int id);
    }
}