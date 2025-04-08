using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace IPLAwardManagementSystem.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamDto> CreateTeamAsync(TeamCreateDto teamCreateDto)
        {
            var team = _mapper.Map<Team>(teamCreateDto);
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _context.Teams.ToListAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.TeamId == id);

            if (team == null) throw new KeyNotFoundException("Team not found");
            return _mapper.Map<TeamDto>(team);
        }

        public async Task UpdateTeamAsync(int id, TeamUpdateDto teamUpdateDto)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) throw new KeyNotFoundException("Team not found");

            _mapper.Map(teamUpdateDto, team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) throw new KeyNotFoundException("Team not found");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerDto>> GetTeamPlayersAsync(int teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }
    }
}