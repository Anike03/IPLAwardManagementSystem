using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using System.Collections.Generic;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IMatchService
    {
        IEnumerable<Match> GetAllMatches();
        Match? GetMatchById(int id);
        void CreateMatch(MatchDTO matchDTO);
        void UpdateMatch(int id, MatchDTO matchDTO);
        void DeleteMatch(int id);
    }
}