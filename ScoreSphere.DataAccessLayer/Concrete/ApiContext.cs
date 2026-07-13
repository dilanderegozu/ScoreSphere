using Microsoft.EntityFrameworkCore;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.DataAccessLayer.Concrete
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<MatchStat> MatchStats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

 
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.League)
                .WithMany(l => l.Matches)
                .HasForeignKey(m => m.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Season)
                .WithMany(s => s.Matches)
                .HasForeignKey(m => m.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

        
            modelBuilder.Entity<MatchStat>()
                .HasOne(ms => ms.Match)
                .WithOne(m => m.MatchStat)
                .HasForeignKey<MatchStat>(ms => ms.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MatchStat>()
                .HasIndex(ms => ms.MatchId)
                .IsUnique();

        
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.Match)
                .WithMany(m => m.Goals)
                .HasForeignKey(g => g.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Goal>()
                .HasOne(g => g.Team)
                .WithMany()
                .HasForeignKey(g => g.TeamId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<MatchEvent>()
                .HasOne(me => me.Match)
                .WithMany(m => m.MatchEvents)
                .HasForeignKey(me => me.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

      
            modelBuilder.Entity<Team>().Property(t => t.TeamName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Team>().Property(t => t.City).HasMaxLength(100);
            modelBuilder.Entity<Team>().Property(t => t.Stadium).HasMaxLength(150);
            modelBuilder.Entity<Team>().Property(t => t.LogoUrl).HasMaxLength(500);

            modelBuilder.Entity<League>().Property(l => l.LeagueName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<League>().Property(l => l.Country).HasMaxLength(100);
            modelBuilder.Entity<League>().Property(l => l.LogoUrl).HasMaxLength(500);

            modelBuilder.Entity<Season>().Property(s => s.SeasonName).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<Match>().Property(m => m.Stadium).HasMaxLength(150);
            modelBuilder.Entity<Match>().Property(m => m.Referee).HasMaxLength(100);

            modelBuilder.Entity<Goal>().Property(g => g.PlayerName).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<MatchEvent>().Property(me => me.PlayerName).HasMaxLength(100);
            modelBuilder.Entity<MatchEvent>().Property(me => me.Description).HasMaxLength(500);
        }
    }
}
