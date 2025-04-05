using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using System.Collections.Generic;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface ITeamService
    {
        IEnumerable<Team> AllTeams { get; }

        Team? GetTeamById(int id); // Add nullable return type
        void CreateTeam(TeamDTO teamDTO);
        void UpdateTeam(int id, TeamDTO teamDTO);
        void DeleteTeam(int id);
    }
}