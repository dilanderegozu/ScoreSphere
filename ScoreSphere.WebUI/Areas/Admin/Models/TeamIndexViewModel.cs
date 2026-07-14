namespace ScoreSphere.WebUI.Areas.Admin.Models
{
    public class TeamIndexViewModel
    {
        public int TotalTeams { get; set; }
        public int TotalCities { get; set; }
        public int TotalStadiums { get; set; }
        public List<TeamListItemViewModel> Teams { get; set; } = new();
    }

    public class TeamListItemViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string LogoUrl { get; set; }
        public string City { get; set; }
        public string Stadium { get; set; }
        public int MatchCount { get; set; }
    }
}