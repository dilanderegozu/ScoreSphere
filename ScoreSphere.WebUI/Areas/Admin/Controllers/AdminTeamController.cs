using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Areas.Admin.Models;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.Dtos.TeamDtos;
using System.Net.Http.Json;

namespace ScoreSphere.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTeamController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminTeamController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            var teamsResponse = await client.GetAsync("api/Team");
            var teams = await teamsResponse.Content.ReadFromJsonAsync<List<ResultTeamDto>>() ?? new();

            var matchesResponse = await client.GetAsync("api/Match");
            var matches = await matchesResponse.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();

            var teamItems = teams.Select(t => new TeamListItemViewModel
            {
                TeamId = t.TeamId,
                TeamName = t.TeamName,
                LogoUrl = t.LogoUrl,
                City = t.City,
                Stadium = t.Stadium,
                MatchCount = matches.Count(m => m.HomeTeamId == t.TeamId || m.AwayTeamId == t.TeamId)
            }).ToList();

            var viewModel = new TeamIndexViewModel
            {
                TotalTeams = teams.Count,
                TotalCities = teams.Select(t => t.City).Distinct().Count(),
                TotalStadiums = teams.Select(t => t.Stadium).Distinct().Count(),
                Teams = teamItems
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.PostAsJsonAsync("api/Team", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var errorBody = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, "Takım eklenirken hata oluştu: " + errorBody);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.GetAsync($"api/Team/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var team = await response.Content.ReadFromJsonAsync<UpdateTeamDto>();
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTeamDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.PutAsJsonAsync("api/Team", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var errorBody = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, "Takım güncellenirken hata oluştu: " + errorBody);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.DeleteAsync($"api/Team/{id}");

            if (response.IsSuccessStatusCode)
                return Ok();

            return StatusCode((int)response.StatusCode);
        }
    }
}