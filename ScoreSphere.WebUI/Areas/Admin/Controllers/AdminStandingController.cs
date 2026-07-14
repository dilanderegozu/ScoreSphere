using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Areas.Admin.Models;
using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Dtos.SeasonDtos;
using ScoreSphere.WebUI.Dtos.StandingDtos;
using ScoreSphere.WebUI.Models;
using System.Net.Http.Json;

namespace ScoreSphere.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminStandingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminStandingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? seasonId, int? leagueId)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            var leaguesResponse = await client.GetAsync("api/League");
            var leagues = await leaguesResponse.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();

            var seasonsResponse = await client.GetAsync("api/Season");
            var allSeasons = await seasonsResponse.Content.ReadFromJsonAsync<List<ResultSeasonDto>>() ?? new();

            var matchesResponse = await client.GetAsync("api/Match");
            var allMatches = await matchesResponse.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();

            int targetLeagueId = leagueId ?? leagues.FirstOrDefault()?.LeagueId ?? 0;

            var leagueSeasons = allSeasons
                .Where(s => s.LeagueId == targetLeagueId)
                .OrderByDescending(s => s.StartDate)
                .ToList();

            int targetSeasonId = (seasonId.HasValue && leagueSeasons.Any(s => s.SeasonId == seasonId.Value))
                ? seasonId.Value
                : (leagueSeasons.FirstOrDefault(s => s.IsActive)?.SeasonId ?? leagueSeasons.FirstOrDefault()?.SeasonId ?? 0);

            var standingResponse = await client.GetAsync($"api/Standing/season/{targetSeasonId}");
            var standings = await standingResponse.Content.ReadFromJsonAsync<List<ResultStandingDto>>() ?? new();

            var currentLeague = leagues.FirstOrDefault(l => l.LeagueId == targetLeagueId);

            var viewModel = new AdminStandingViewModel
            {
                SeasonId = targetSeasonId,
                LeagueId = targetLeagueId,
                LeagueName = currentLeague?.LeagueName ?? "",
                Leagues = leagues,
                Seasons = leagueSeasons,
                Standings = standings
            };

            return View(viewModel);
        }
    }
}