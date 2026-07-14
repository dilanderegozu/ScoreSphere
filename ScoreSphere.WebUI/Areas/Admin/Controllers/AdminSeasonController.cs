using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Areas.Admin.Dtos.SeasonDtos;
using ScoreSphere.WebUI.Areas.Admin.Models;
using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.SeasonDtos;
using System.Net.Http.Json;

namespace ScoreSphere.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSeasonController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminSeasonController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            var seasonsResponse = await client.GetAsync("api/Season");
            var seasons = await seasonsResponse.Content.ReadFromJsonAsync<List<ResultSeasonDto>>() ?? new();

            var leaguesResponse = await client.GetAsync("api/League");
            var leagues = await leaguesResponse.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();

            var now = DateTime.Now;

            var seasonItems = seasons.Select(s =>
            {
                string status;
                if (now < s.StartDate)
                    status = "Planlandı";
                else if (now <= s.EndDate)
                    status = "Devam Ediyor";
                else
                    status = "Tamamlandı";

                var league = leagues.FirstOrDefault(l => l.LeagueId == s.LeagueId);

                return new SeasonListItemViewModel
                {
                    SeasonId = s.SeasonId,
                    SeasonName = s.SeasonName,
                    LeagueName = league?.LeagueName ?? "Bilinmiyor",
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Status = status
                };
            }).ToList();

            var viewModel = new SeasonIndexViewModel
            {
                ActiveSeasons = seasonItems.Count(s => s.Status == "Devam Ediyor"),
                UpcomingSeasons = seasonItems.Count(s => s.Status == "Planlandı"),
                CompletedSeasons = seasonItems.Count(s => s.Status == "Tamamlandı"),
                Seasons = seasonItems
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLeagues();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSeasonDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadLeagues();
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.PostAsJsonAsync("api/Season", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var errorBody = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, "Sezon eklenirken hata oluştu: " + errorBody);
            await LoadLeagues();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.GetAsync($"api/Season/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var season = await response.Content.ReadFromJsonAsync<UpdateSeasonDto>();
            await LoadLeagues();
            return View(season);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSeasonDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadLeagues();
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.PutAsJsonAsync("api/Season", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var errorBody = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, "Sezon güncellenirken hata oluştu: " + errorBody);
            await LoadLeagues();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.DeleteAsync($"api/Season/{id}");

            if (response.IsSuccessStatusCode)
                return Ok();

            return StatusCode((int)response.StatusCode);
        }

        private async Task LoadLeagues()
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.GetAsync("api/League");
            var leagues = await response.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();
            ViewBag.Leagues = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(leagues, "LeagueId", "LeagueName");
        }
    }
}