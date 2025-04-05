using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using System.Collections.Generic;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IPlayerService
    {
        IEnumerable<Player> GetAllPlayers();
        Player? GetPlayerById(int id);
        void CreatePlayer(PlayerDTO playerDTO);
        void UpdatePlayer(int id, PlayerDTO playerDTO);
        void DeletePlayer(int id);
    }
}