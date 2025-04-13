using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IVoteService
    {
        Task<IEnumerable<VoteDto>> GetAllVotesAsync();
        Task<VoteDto?> GetVoteByIdAsync(int id);
        Task<VoteDto> CreateVoteAsync(VoteCreateDto dto);
        Task DeleteVoteAsync(int id);

        Task<int> GetTotalVotesForPlayerAsync(int awardId, int playerId);
        Task<IEnumerable<VoteDto>> GetVotesByAwardAsync(int awardId);
        Task<IEnumerable<VoteDto>> GetVotesByPlayerAsync(int playerId);
        Task<IEnumerable<VoteDto>> GetVotesByVoterAsync(int voterId);
    }
}