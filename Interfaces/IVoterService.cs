using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IVoterService
    {
        Task<IEnumerable<VoterDto>> GetAllVotersAsync();
        Task<VoterDto?> GetVoterByIdAsync(int id);
        Task<VoterDto> CreateVoterAsync(VoterCreateDto dto);
        Task UpdateVoterAsync(int id, VoterUpdateDto dto);
        Task DeleteVoterAsync(int id);
        Task<IEnumerable<VoteDto>> GetVotesByVoterAsync(int voterId);
    }
}