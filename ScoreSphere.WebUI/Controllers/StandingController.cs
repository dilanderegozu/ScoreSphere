using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Dtos.SeasonDtos;
using ScoreSphere.WebUI.Dtos.StandingDtos;
using ScoreSphere.WebUI.Models;
using ScoreSphere.WebUI.ViewModels;
using System.Net.Http.Json;

namespace ScoreSphere.WebUI.Controllers
{
    public class StandingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StandingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? seasonId)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            var leaguesResponse = await client.GetAsync("api/League");
            var leagues = await leaguesResponse.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();

            var seasonsResponse = await client.GetAsync("api/Season");
            var allSeasons = await seasonsResponse.Content.ReadFromJsonAsync<List<ResultSeasonDto>>() ?? new();

            var allMatchesResponse = await client.GetAsync("api/Match");
            var allMatches = await allMatchesResponse.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();

            var leagueFilters = leagues
                .Select(l => new LeagueFilterItem
                {
                    LeagueId = l.LeagueId,
                    LeagueName = l.LeagueName,
                    MatchCount = allMatches.Count(m => m.LeagueId == l.LeagueId),
                    SeasonId = allSeasons.FirstOrDefault(s => s.LeagueId == l.LeagueId && s.IsActive)?.SeasonId ?? 0
                })
                .ToList();

            int targetSeasonId = seasonId ?? leagueFilters.FirstOrDefault()?.SeasonId ?? 0;

            var standingResponse = await client.GetAsync($"api/Standing/season/{targetSeasonId}");
            var standings = await standingResponse.Content.ReadFromJsonAsync<List<ResultStandingDto>>() ?? new();

            var currentSeason = allSeasons.FirstOrDefault(s => s.SeasonId == targetSeasonId);
            var currentLeague = leagues.FirstOrDefault(l => l.LeagueId == currentSeason?.LeagueId);

            var viewModel = new StandingIndexViewModel
            {
                SeasonId = targetSeasonId,
                LeagueName = currentLeague?.LeagueName ?? "",
                Country = currentLeague?.Country ?? "",
                LogoUrl = currentLeague?.LogoUrl ?? "",
                SeasonName = currentSeason?.SeasonName ?? "",
                CurrentLeagueId = currentLeague?.LeagueId ?? 0,
                Leagues = leagueFilters,
                Standings = standings
            };

            return View(viewModel);
        }
    }
}