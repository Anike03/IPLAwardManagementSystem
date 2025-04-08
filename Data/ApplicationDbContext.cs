using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IPLAwardManagementSystem.Models;

namespace IPLAwardManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Award> Awards { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerAward> PlayerAwards { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Voter> Voters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important for Identity configuration

            // Configure many-to-many relationship between Match and Team
            modelBuilder.Entity<Match>()
                .HasMany(m => m.Teams)
                .WithMany(t => t.Matches)
                .UsingEntity(j => j.ToTable("MatchTeam"));

            // Configure composite primary key for PlayerAward
            modelBuilder.Entity<PlayerAward>()
                .HasKey(pa => new { pa.PlayerId, pa.AwardId });

            // Configure relationships for PlayerAward
            modelBuilder.Entity<PlayerAward>()
                .HasOne(pa => pa.Player)
                .WithMany(p => p.PlayerAwards)
                .HasForeignKey(pa => pa.PlayerId);

            modelBuilder.Entity<PlayerAward>()
                .HasOne(pa => pa.Award)
                .WithMany(a => a.PlayerAwards)
                .HasForeignKey(pa => pa.AwardId);

            // Configure relationships for Vote
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Award)
                .WithMany(a => a.Votes)
                .HasForeignKey(v => v.AwardId);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Player)
                .WithMany()
                .HasForeignKey(v => v.PlayerId);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Voter)
                .WithMany(v => v.Votes)
                .HasForeignKey(v => v.VoterId);
        }
    }
}