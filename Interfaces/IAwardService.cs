// Interfaces/IAwardService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IAwardService
    {
        Task<AwardDto> CreateAwardAsync(AwardCreateDto awardCreateDto);
        Task<IEnumerable<AwardDto>> GetAllAwardsAsync(bool? isActive = null);
        Task<AwardDto> GetAwardByIdAsync(int id);
        Task UpdateAwardAsync(int id, AwardUpdateDto awardUpdateDto);
        Task DeleteAwardAsync(int id);
        Task ToggleAwardStatusAsync(int id);
    }
}