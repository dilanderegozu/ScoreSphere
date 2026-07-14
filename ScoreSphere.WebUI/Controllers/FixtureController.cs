using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Dtos.SeasonDtos;
using ScoreSphere.WebUI.Models;
using ScoreSphere.WebUI.ViewModels;
using System.Net.Http.Json;

namespace ScoreSphere.WebUI.Controllers
    {
        public class FixtureController : Controller
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public FixtureController(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

        public async Task<IActionResult> Index(int seasonId, string week = "")
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            var allMatchesResponse = await client.GetAsync("api/Match");
            var allMatches = await allMatchesResponse.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();
            var seasonMatches = allMatches.Where(m => m.SeasonId == seasonId).ToList();

            var availableWeeks = seasonMatches
                .Select(m => m.Week)
                .Distinct()
                .OrderBy(w => w)
                .ToList();

            List<ResultMatchDto> weekMatches;
            string actualWeek;

            if (week == "all")
            {
                weekMatches = seasonMatches;
                actualWeek = "all";
            }
            else if (int.TryParse(week, out int parsedWeek) && availableWeeks.Contains(parsedWeek))
            {
                var weekResponse = await client.GetAsync($"api/Match/season/{seasonId}/week/{parsedWeek}");
                weekMatches = await weekResponse.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();
                actualWeek = parsedWeek.ToString();
            }
            else
            {
                var firstWeek = availableWeeks.FirstOrDefault();
                var weekResponse = await client.GetAsync($"api/Match/season/{seasonId}/week/{firstWeek}");
                weekMatches = await weekResponse.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();
                actualWeek = firstWeek.ToString();
            }

            var dayGroups = weekMatches
                .GroupBy(m => m.MatchDate.Date)
                .OrderBy(g => g.Key)
                .Select(g => new FixtureDayGroup
                {
                    Date = g.Key,
                    Matches = g.OrderBy(m => m.MatchDate).ToList()
                })
                .ToList();

            var bigMatch = allMatches
                .Where(m => m.IsFeatured && m.Status == "Upcoming")
                .OrderBy(m => m.MatchDate)
                .FirstOrDefault();

            var leaguesResponse = await client.GetAsync("api/League");
            var leagues = await leaguesResponse.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();

            var seasonsResponse = await client.GetAsync("api/Season");
            var allSeasons = await seasonsResponse.Content.ReadFromJsonAsync<List<ResultSeasonDto>>() ?? new();

            var leagueFilters = leagues
                .Select(l => new LeagueFilterItem
                {
                    LeagueId = l.LeagueId,
                    LeagueName = l.LeagueName,
                    MatchCount = allMatches.Count(m => m.LeagueId == l.LeagueId),
                    SeasonId = allSeasons.FirstOrDefault(s => s.LeagueId == l.LeagueId && s.IsActive)?.SeasonId ?? 0
                })
                .ToList();

            var viewModel = new FixtureViewModel
            {
                SeasonId = seasonId,
                LeagueName = seasonMatches.FirstOrDefault()?.LeagueName ?? "",
                CurrentWeek = actualWeek,
                AvailableWeeks = availableWeeks,
                DayGroups = dayGroups,
                BigMatch = bigMatch,
                Leagues = leagueFilters,
                CurrentLeagueId = seasonMatches.FirstOrDefault()?.LeagueId ?? 0,
            };

            return View(viewModel);
        }
    }
    }
