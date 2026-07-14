namespace ScoreSphere.WebUI.Areas.Admin.Models
{
    public class LeagueIndexViewModel
    {
        public int TotalLeagues { get; set; }
        public int TotalTeams { get; set; }
        public List<LeagueListItemViewModel> Leagues { get; set; } = new();
    }
}
