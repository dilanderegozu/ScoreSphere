using ScoreSphere.WebUI.Dtos.StandingDtos;
using ScoreSphere.WebUI.Models;

namespace ScoreSphere.WebUI.ViewModels
{
    public class StandingIndexViewModel
    {
        public int SeasonId { get; set; }
        public string LeagueName { get; set; }
        public string Country { get; set; }
        public string LogoUrl { get; set; }
        public string SeasonName { get; set; }
        public int CurrentLeagueId { get; set; }
        public List<LeagueFilterItem> Leagues { get; set; } = new();
        public List<ResultStandingDto> Standings { get; set; } = new();
    }
}