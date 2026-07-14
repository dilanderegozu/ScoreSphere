using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Dtos.StandingDtos;
using ScoreSphere.WebUI.Models;

namespace ScoreSphere.WebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int TotalLeagues { get; set; }
        public int TotalTeams { get; set; }
        public int TotalMatches { get; set; }
        public int ThisWeekMatches { get; set; }
        public List<ResultMatchDto> RecentMatches { get; set; } = new();
        public List<LeagueFilterItem> Leagues { get; set; } = new();
        public int SelectedSeasonId { get; set; }
        public string SelectedLeagueName { get; set; }
        public List<ResultStandingDto> TopStandings { get; set; } = new();
    }
}
