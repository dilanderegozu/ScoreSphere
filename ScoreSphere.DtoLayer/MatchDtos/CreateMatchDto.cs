using ScoreSphere.DtoLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.DtoLayer.MatchDtos
{
    public class CreateMatchDto
    {
  

        public int LeagueId { get; set; }
        public string LeagueName { get; set; }

        public int SeasonId { get; set; }
        public string SeasonName { get; set; }

        public int Week { get; set; }

        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamLogoUrl { get; set; }

        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamLogoUrl { get; set; }

        public DateTime MatchDate { get; set; }
        public string Stadium { get; set; }
        public string Referee { get; set; }

        public MatchStatus Status { get; set; }
        public int Minute { get; set; }
        public bool IsFeatured { get; set; }

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public int? HomeHalfScore { get; set; }
        public int? AwayHalfScore { get; set; }
    }
}
