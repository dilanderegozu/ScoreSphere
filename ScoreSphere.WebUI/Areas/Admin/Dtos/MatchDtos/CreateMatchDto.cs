namespace ScoreSphere.WebUI.Areas.Admin.Dtos.MatchDtos
{
    public class CreateMatchDto
    {
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int Week { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public DateTime MatchDate { get; set; }
        public string Stadium { get; set; }
        public string Referee { get; set; }

        public bool IsFeatured { get; set; }
    }
}
