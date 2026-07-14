namespace ScoreSphere.WebUI.Areas.Admin.Models
{
    public class LeagueListItemViewModel
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string Country { get; set; }
        public string LogoUrl { get; set; }
        public int MatchCount { get; set; }
        public string SeasonName { get; set; }
        public string Status { get; set; }

        public int TeamCount { get; set; }
    }
}
