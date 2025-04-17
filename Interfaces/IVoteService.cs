using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IVoteService
    {
        Task<IEnumerable<VoteDto>> GetAllVotesAsync();
        Task<VoteDto?> GetVoteByIdAsync(int id);
        Task<VoteDto> CreateVoteAsync(VoteCreateDto dto);
        Task UpdateVoteAsync(int id, VoteUpdateDto dto);
        Task DeleteVoteAsync(int id);
        Task<IEnumerable<VoteResultDto>> GetVoteResultsAsync();

    }
}