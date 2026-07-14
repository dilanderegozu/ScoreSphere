using ScoreSphere.WebUI.Dtos.MatchDtos;

namespace ScoreSphere.WebUI.ViewModels
{
    public class LeagueMatchesViewModel
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string Country { get; set; }
        public string LogoUrl { get; set; }
        public List<ResultMatchDto> Matches { get; set; } = new();
    }
}