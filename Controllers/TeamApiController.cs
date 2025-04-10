﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;

namespace IPLAwardManagementSystem.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.Include(t => t.Players).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.Include(t => t.Players)
                                           .FirstOrDefaultAsync(t => t.TeamId == id);
            if (team == null) return NotFound();
            return team;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.TeamId) return BadRequest();

            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return NotFound();

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
