// Interfaces/IVoterService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IVoterService
    {
        Task<VoterDto> CreateVoterAsync(VoterCreateDto voterCreateDto);
        Task<IEnumerable<VoterDto>> GetAllVotersAsync();
        Task<VoterDto> GetVoterByIdAsync(int id);
        Task UpdateVoterAsync(int id, VoterUpdateDto voterUpdateDto);
        Task DeleteVoterAsync(int id);
        Task VerifyVoterAsync(int id);
        Task<IEnumerable<VoteDto>> GetVotesByVoterAsync(int voterId);
    }
}