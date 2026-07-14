using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Dtos.MatchDtos;

namespace ScoreSphere.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");
            var response = await client.GetAsync("api/Match/status/Live");

            ResultMatchDto? featuredMatch = null;

            if (response.IsSuccessStatusCode)
            {
                var matches = await response.Content.ReadFromJsonAsync<List<ResultMatchDto>>();
                featuredMatch = matches?.FirstOrDefault(m => m.IsFeatured) ?? matches?.FirstOrDefault();
            }

            return View(featuredMatch);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }

}
