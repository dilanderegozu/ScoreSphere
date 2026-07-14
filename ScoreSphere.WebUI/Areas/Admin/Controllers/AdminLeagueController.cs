using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Areas.Admin.Models;
using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Dtos.SeasonDtos;

namespace ScoreSphere.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLeagueController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public AdminLeagueController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            var leaguesResponse = await client.GetAsync("api/League");
            var leagues = await leaguesResponse.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();

            var seasonsResponse = await client.GetAsync("api/Season");
            var allSeasons = await seasonsResponse.Content.ReadFromJsonAsync<List<ResultSeasonDto>>() ?? new();

            var matchesResponse = await client.GetAsync("api/Match");
            var allMatches = await matchesResponse.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();

            var now = DateTime.Now;

            var leagueItems = leagues.Select(l =>
            {
                var activeSeason = allSeasons.FirstOrDefault(s => s.LeagueId == l.LeagueId && s.IsActive)
                                    ?? allSeasons.Where(s => s.LeagueId == l.LeagueId).OrderByDescending(s => s.StartDate).FirstOrDefault();

                string status = "Bilinmiyor";
                if (activeSeason != null)
                {
                    if (now < activeSeason.StartDate)
                        status = "Planlandı";
                    else if (now <= activeSeason.EndDate)
                        status = "Devam Ediyor";
                    else
                        status = "Tamamlandı";
                }
                var leagueMatches = allMatches.Where(m => m.LeagueId == l.LeagueId).ToList();
                var uniqueTeamCount = leagueMatches
                    .SelectMany(m => new[] { m.HomeTeamId, m.AwayTeamId })
                    .Distinct()
                    .Count();

                return new LeagueListItemViewModel
                {
                    LeagueId = l.LeagueId,
                    LeagueName = l.LeagueName,
                    Country = l.Country,
                    LogoUrl = l.LogoUrl,
                    MatchCount = allMatches.Count(m => m.LeagueId == l.LeagueId),
                    TeamCount = uniqueTeamCount,
                    SeasonName = activeSeason?.SeasonName ?? "Sezon yok",
                    Status = status
                };
            }).ToList();

            // Buraya eksik olan Team çekme + sarmalama ekleniyor
            var teamsResponse = await client.GetAsync("api/Team");
            var allTeams = await teamsResponse.Content.ReadFromJsonAsync<List<ScoreSphere.WebUI.Dtos.TeamDtos.ResultTeamDto>>() ?? new();

            var viewModel = new LeagueIndexViewModel
            {
                TotalLeagues = leagues.Count,
                TotalTeams = allTeams.Count,
                Leagues = leagueItems
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateLeague()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeague(CreateLeagueDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.PostAsJsonAsync("api/League", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var errorBody = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, "Lig eklenirken hata oluştu: " + errorBody);
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.GetAsync($"api/League/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var league = await response.Content.ReadFromJsonAsync<UpdateLeagueDto>();
            return View(league);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateLeagueDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.PutAsJsonAsync("api/League", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var errorBody = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, "Lig güncellenirken hata oluştu: " + errorBody);
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.DeleteAsync($"api/League/{id}");

            if (response.IsSuccessStatusCode)
                return Ok();

            return StatusCode((int)response.StatusCode);
        }
    }
}