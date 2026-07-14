using Microsoft.EntityFrameworkCore;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
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

      
        }
    }
}















// Eğer bu takımı silmeye çalışırsan ve bu takım maçlarda kullanılıyorsa silme.