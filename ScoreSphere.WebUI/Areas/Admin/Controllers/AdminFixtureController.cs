using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScoreSphere.WebUI.Areas.Admin.Dtos.MatchDtos;
using ScoreSphere.WebUI.Areas.Admin.Models;
using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Dtos.SeasonDtos;
using ScoreSphere.WebUI.Dtos.TeamDtos;
using ScoreSphere.WebUI.Models;
using System.Net.Http.Json;

namespace ScoreSphere.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminFixtureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminFixtureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? seasonId, string week = "")
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
                    SeasonId = allSeasons.FirstOrDefault(s => s.LeagueId == l.LeagueId && s.IsActive)?.SeasonId
                               ?? allSeasons.Where(s => s.LeagueId == l.LeagueId).OrderByDescending(s => s.StartDate).FirstOrDefault()?.SeasonId
                               ?? 0
                })
                .ToList();

            int targetSeasonId = seasonId ?? leagueFilters.FirstOrDefault()?.SeasonId ?? 0;

            var seasonMatches = allMatches.Where(m => m.SeasonId == targetSeasonId).ToList();

            var availableWeeks = seasonMatches
                .Select(m => m.Week)
                .Distinct()
                .OrderBy(w => w)
                .ToList();

            List<ResultMatchDto> displayMatches;
            string currentWeekValue;

            if (week == "all")
            {
                displayMatches = seasonMatches.OrderBy(m => m.MatchDate).ToList();
                currentWeekValue = "all";
            }
            else if (int.TryParse(week, out int parsedWeek) && availableWeeks.Contains(parsedWeek))
            {
                displayMatches = seasonMatches.Where(m => m.Week == parsedWeek).OrderBy(m => m.MatchDate).ToList();
                currentWeekValue = parsedWeek.ToString();
            }
            else
            {
                var firstWeek = availableWeeks.FirstOrDefault();
                displayMatches = seasonMatches.Where(m => m.Week == firstWeek).OrderBy(m => m.MatchDate).ToList();
                currentWeekValue = firstWeek.ToString();
            }

            var currentSeason = allSeasons.FirstOrDefault(s => s.SeasonId == targetSeasonId);
            var currentLeague = leagues.FirstOrDefault(l => l.LeagueId == currentSeason?.LeagueId);

            var viewModel = new FixtureManagementViewModel
            {
                SeasonId = targetSeasonId,
                LeagueName = currentLeague?.LeagueName ?? "",
                CurrentWeek = currentWeekValue,
                AvailableWeeks = availableWeeks,
                Leagues = leagueFilters,
                CurrentLeagueId = currentLeague?.LeagueId ?? 0,
                Matches = displayMatches
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? seasonId)
        {
            await LoadDropdowns(seasonId);

            var dto = new CreateMatchDto();
            if (seasonId.HasValue)
                dto.SeasonId = seasonId.Value;

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMatchDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(dto.SeasonId);
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.PostAsJsonAsync("api/Match", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { seasonId = dto.SeasonId });

            var errorBody = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, "Maç eklenirken hata oluştu: " + errorBody);
            await LoadDropdowns(dto.SeasonId);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.GetAsync($"api/Match/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var match = await response.Content.ReadFromJsonAsync<UpdateMatchDto>();
            await LoadDropdowns(match?.SeasonId);
            return View(match);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateMatchDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(dto.SeasonId);
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.PutAsJsonAsync("api/Match", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { seasonId = dto.SeasonId });

            var errorBody = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, "Maç güncellenirken hata oluştu: " + errorBody);
            await LoadDropdowns(dto.SeasonId);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.DeleteAsync($"api/Match/{id}");

            if (response.IsSuccessStatusCode)
                return Ok();

            return StatusCode((int)response.StatusCode);
        }

        private async Task LoadDropdowns(int? seasonId)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            var leaguesResponse = await client.GetAsync("api/League");
            var leagues = await leaguesResponse.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();
            ViewBag.Leagues = new SelectList(leagues, "LeagueId", "LeagueName");

            var seasonsResponse = await client.GetAsync("api/Season");
            var seasons = await seasonsResponse.Content.ReadFromJsonAsync<List<ResultSeasonDto>>() ?? new();
            ViewBag.Seasons = new SelectList(seasons, "SeasonId", "SeasonName", seasonId);

            var teamsResponse = await client.GetAsync("api/Team");
            var teams = await teamsResponse.Content.ReadFromJsonAsync<List<ResultTeamDto>>() ?? new();
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName");
        }
    }
}