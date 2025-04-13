using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamListDto>> GetAllTeamsAsync();
        Task<TeamDetailDto?> GetTeamByIdAsync(int id);
        Task<TeamDetailDto> CreateTeamAsync(TeamCreateDto dto);
        Task UpdateTeamAsync(int id, TeamUpdateDto dto);
        Task DeleteTeamAsync(int id);
        Task<IEnumerable<PlayerListDto>> GetPlayersByTeamIdAsync(int teamId);
    }
}