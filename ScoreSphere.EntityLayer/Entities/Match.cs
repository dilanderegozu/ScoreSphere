using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.EntityLayer.Entities
{
    public class Match
    {
        public int MatchId { get; set; }

        public int LeagueId { get; set; }
        public League? League { get; set; }

        public int SeasonId { get; set; }
        public Season? Season { get; set; }

        public int Week { get; set; }

        public int HomeTeamId { get; set; }
        public Team? HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team? AwayTeam { get; set; }

        public DateTime MatchDate { get; set; }
        public string? Stadium { get; set; }
        public string? Referee { get; set; }

        public MatchStatus Status { get; set; }
        public int Minute { get; set; }
        public bool IsFeatured { get; set; }

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public int? HomeHalfScore { get; set; }
        public int? AwayHalfScore { get; set; }

        public ICollection<Goal>? Goals { get; set; }
        public ICollection<MatchEvent>? MatchEvents { get; set; }
        public MatchStat? MatchStat { get; set; }
    }
    public enum MatchStatus
    {
        Upcoming,
        Live,
        HalfTime,
        Finished,
        Postponed,
        Cancelled
    }
}
