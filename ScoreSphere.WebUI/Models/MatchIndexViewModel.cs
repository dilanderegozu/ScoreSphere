using ScoreSphere.WebUI.Dtos.LeagueDtos;

namespace ScoreSphere.WebUI.ViewModels
{
    public class MatchIndexViewModel
    {
        public List<ResultLeagueDto> PopularLeagues { get; set; } = new();
        public List<LeagueMatchesViewModel> LeagueGroups { get; set; } = new();

        public string CurrentFilter { get; set; } = "all";
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
    }
}