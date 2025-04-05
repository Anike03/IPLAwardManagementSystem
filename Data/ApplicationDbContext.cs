using IPLAwardManagementSystem.Models;  // Make sure this matches your project name
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IPLAwardManagementSystem.Data  // Updated to match your project name
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<PlayerAward> PlayerAwards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many Relationship: Match ↔ Teams
            modelBuilder.Entity<Match>()
                .HasMany(m => m.Teams)
                .WithMany(t => t.Matches)
                .UsingEntity<Dictionary<string, object>>(
                    "MatchTeams",
                    j => j.HasOne<Team>().WithMany().HasForeignKey("TeamId"),
                    j => j.HasOne<Match>().WithMany().HasForeignKey("MatchId"),
                    j => j.HasKey("MatchId", "TeamId")
                );

            // One-to-Many Relationship: Venue ↔ Matches
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Venue)
                .WithMany(v => v.Matches)
                .HasForeignKey(m => m.VenueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Awards
            modelBuilder.Entity<Award>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Description).HasMaxLength(500);
            });

            // Configure Voters
            modelBuilder.Entity<Voter>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Name).IsRequired().HasMaxLength(100);
                entity.Property(v => v.Email).IsRequired().HasMaxLength(100);
                entity.Property(v => v.VoterId).IsRequired().HasMaxLength(50);
                entity.Property(v => v.Role).IsRequired().HasMaxLength(20);
                entity.HasIndex(v => v.Email).IsUnique();
                entity.HasIndex(v => v.VoterId).IsUnique();
            });

            // Configure Votes
            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.HasOne(v => v.Award)
                    .WithMany(a => a.Votes)
                    .HasForeignKey(v => v.AwardId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(v => v.Player)
                    .WithMany()
                    .HasForeignKey(v => v.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(v => v.Voter)
                    .WithMany(v => v.Votes)
                    .HasForeignKey(v => v.VoterId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure PlayerAwards
            modelBuilder.Entity<PlayerAward>(entity =>
            {
                entity.HasKey(pa => new { pa.PlayerId, pa.AwardId });

                entity.HasOne(pa => pa.Player)
                    .WithMany(p => p.PlayerAwards)  // Added navigation property reference
                    .HasForeignKey(pa => pa.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pa => pa.Award)
                    .WithMany(a => a.PlayerAwards)
                    .HasForeignKey(pa => pa.AwardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}