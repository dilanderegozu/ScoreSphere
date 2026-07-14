using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Models;

namespace ScoreSphere.WebUI.Areas.Admin.Models
{
    public class FixtureManagementViewModel
    {
        public int SeasonId { get; set; }
        public string LeagueName { get; set; }
        public string CurrentWeek { get; set; }
        public List<int> AvailableWeeks { get; set; } = new();
        public List<LeagueFilterItem> Leagues { get; set; } = new();
        public int CurrentLeagueId { get; set; }
        public List<ResultMatchDto> Matches { get; set; } = new();
    }
}