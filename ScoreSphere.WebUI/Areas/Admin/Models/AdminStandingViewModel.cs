using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.SeasonDtos;
using ScoreSphere.WebUI.Dtos.StandingDtos;

namespace ScoreSphere.WebUI.Areas.Admin.Models
{
    public class AdminStandingViewModel
    {
        public int SeasonId { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public List<ResultLeagueDto> Leagues { get; set; } = new();
        public List<ResultSeasonDto> Seasons { get; set; } = new();
        public List<ResultStandingDto> Standings { get; set; } = new();
    }
}