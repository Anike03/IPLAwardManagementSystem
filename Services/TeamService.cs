using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<TeamDetailDto> CreateTeamAsync(TeamCreateDto dto)
        {
            var team = _mapper.Map<Team>(dto);
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeamDetailDto>(team);
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TeamListDto>> GetAllTeamsAsync()
        {
            var teams = await _context.Teams.Include(t => t.Players).ToListAsync();
            return _mapper.Map<List<TeamListDto>>(teams);
        }

        public async Task<TeamDetailDto?> GetTeamByIdAsync(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.TeamId == id);

            return team == null ? null : _mapper.Map<TeamDetailDto>(team);
        }

        public async Task<IEnumerable<PlayerListDto>> GetPlayersByTeamIdAsync(int teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .ToListAsync();

            return _mapper.Map<List<PlayerListDto>>(players);
        }

        public async Task UpdateTeamAsync(int id, TeamUpdateDto dto)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                // Don't touch team.TeamId
                team.TeamName = dto.TeamName;
                team.Coach = dto.Coach;
                team.HomeCity = dto.HomeCity;
                team.FoundingDate = dto.FoundingDate;

                await _context.SaveChangesAsync();
            }
        }

    }
}