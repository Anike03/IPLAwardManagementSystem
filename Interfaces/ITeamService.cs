// Interfaces/ITeamService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> CreateTeamAsync(TeamCreateDto teamCreateDto);
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto> GetTeamByIdAsync(int id);
        Task UpdateTeamAsync(int id, TeamUpdateDto teamUpdateDto);
        Task DeleteTeamAsync(int id);
        Task<IEnumerable<PlayerDto>> GetTeamPlayersAsync(int teamId);
    }
}