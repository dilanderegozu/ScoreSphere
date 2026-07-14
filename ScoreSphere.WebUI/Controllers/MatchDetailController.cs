using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Dtos.MatchDtos;

namespace ScoreSphere.WebUI.Controllers
{
    public class MatchDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MatchDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.GetAsync($"api/Match/{id}/detail");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            else
            {
                var matchDetail = await response.Content.ReadFromJsonAsync<MatchDetailDto>();
                return View(matchDetail);
            }
        }
    }
}
