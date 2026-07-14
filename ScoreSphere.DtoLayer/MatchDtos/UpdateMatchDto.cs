namespace ScoreSphere.DtoLayer.MatchDtos
{
    public class UpdateMatchDto
    {
        public int MatchId { get; set; }

        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int Week { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public DateTime MatchDate { get; set; }
        public string Stadium { get; set; }
        public string Referee { get; set; }

        public string Status { get; set; }
        public int Minute { get; set; }
        public bool IsFeatured { get; set; }

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public int? HomeHalfScore { get; set; }
        public int? AwayHalfScore { get; set; }
    }
}