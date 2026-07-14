using ScoreSphere.WebUI.Dtos.MatchDtos;

namespace ScoreSphere.WebUI.Models
{
    public class FixtureViewModel
    {
        public int SeasonId { get; set; }
        public string LeagueName { get; set; }
        public string CurrentWeek { get; set; }
        public List<int> AvailableWeeks { get; set; } = new();

        public List<FixtureDayGroup> DayGroups { get; set; } = new();

        public ResultMatchDto? BigMatch { get; set; }

        public List<LeagueFilterItem> Leagues { get; set; } = new();
        public int CurrentLeagueId { get; set; }
    }

    public class FixtureDayGroup
    {
        public DateTime Date { get; set; }
        public List<ResultMatchDto> Matches { get; set; } = new();
    }

    public class LeagueFilterItem
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public int MatchCount { get; set; }
        public int SeasonId { get; set; }
    }
}