using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Dtos.TeamDtos;
using System.Net.Http.Json;

namespace ScoreSphere.WebUI.Controllers
{
    public class TeamController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TeamController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> TeamList()
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.GetAsync("api/Team");
            var teams = await response.Content.ReadFromJsonAsync<List<ResultTeamDto>>();
            return View(teams);
        }
    }
}