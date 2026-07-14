using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Areas.Admin.Models;
using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Dtos.SeasonDtos;
using ScoreSphere.WebUI.Dtos.StandingDtos;
using ScoreSphere.WebUI.Dtos.TeamDtos;
using ScoreSphere.WebUI.Models;



namespace ScoreSphere.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? seasonId)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            var leaguesResponse = await client.GetAsync("api/League");
            var leagues = await leaguesResponse.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();

            var teamsResponse = await client.GetAsync("api/Team");
            var teams = await teamsResponse.Content.ReadFromJsonAsync<List<ResultTeamDto>>() ?? new();

            var matchesResponse = await client.GetAsync("api/Match");
            var matches = await matchesResponse.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();

            var seasonsResponse = await client.GetAsync("api/Season");
            var allSeasons = await seasonsResponse.Content.ReadFromJsonAsync<List<ResultSeasonDto>>() ?? new();

            var leagueFilters = leagues
                .Select(l => new LeagueFilterItem
                {
                    LeagueId = l.LeagueId,
                    LeagueName = l.LeagueName,
                    MatchCount = matches.Count(m => m.LeagueId == l.LeagueId),
                    SeasonId = allSeasons.FirstOrDefault(s => s.LeagueId == l.LeagueId && s.IsActive)?.SeasonId ?? 0
                })
                .ToList();

            int targetSeasonId = seasonId ?? leagueFilters.FirstOrDefault()?.SeasonId ?? 0;

            var standingResponse = await client.GetAsync($"api/Standing/season/{targetSeasonId}");
            var standings = await standingResponse.Content.ReadFromJsonAsync<List<ResultStandingDto>>() ?? new();

            var selectedSeason = allSeasons.FirstOrDefault(s => s.SeasonId == targetSeasonId);
            var selectedLeague = leagues.FirstOrDefault(l => l.LeagueId == selectedSeason?.LeagueId);

            var now = DateTime.Now;
            var startOfWeek = now.AddDays(-(int)now.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            var viewModel = new DashboardViewModel
            {
                TotalLeagues = leagues.Count,
                TotalTeams = teams.Count,
                TotalMatches = matches.Count(m => m.Status == "Finished"),
                ThisWeekMatches = matches.Count(m => m.MatchDate >= startOfWeek && m.MatchDate < endOfWeek),
                RecentMatches = matches
                    .Where(m => m.Status == "Live" || m.Status == "Finished")
                    .OrderByDescending(m => m.MatchDate)
                    .Take(4)
                    .ToList(),
                Leagues = leagueFilters,
                SelectedSeasonId = targetSeasonId,
                SelectedLeagueName = selectedLeague?.LeagueName ?? "",
                TopStandings = standings.OrderByDescending(s => s.Points).Take(5).ToList()
            };

            return View(viewModel);
        }
    }
}

