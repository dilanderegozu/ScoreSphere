using Microsoft.AspNetCore.Mvc;
using ScoreSphere.WebUI.Dtos.LeagueDtos;
using ScoreSphere.WebUI.Dtos.MatchDtos;
using ScoreSphere.WebUI.ViewModels;

namespace ScoreSphere.WebUI.Controllers
{
    public class MatchController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MatchController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string filter = "all", int page = 1)
        {
            var client = _httpClientFactory.CreateClient("ScoreSphereApi");

            List<ResultMatchDto> matches;

            if (filter == "all")
            {
                var live = await GetMatchesByStatus(client, "Live");
                var finished = await GetMatchesByStatus(client, "Finished");
                var upcoming = await GetMatchesByStatus(client, "Upcoming");
                matches = live.Concat(finished).Concat(upcoming).ToList();
            }
            else
            {
                matches = await GetMatchesByStatus(client, filter);
            }

            var leaguesResponse = await client.GetAsync("api/League");
            var leagues = await leaguesResponse.Content.ReadFromJsonAsync<List<ResultLeagueDto>>() ?? new();

            const int pageSize = 4;
            var totalCount = matches.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedMatches = matches
                .OrderBy(m => m.MatchDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var leagueGroups = pagedMatches
                .GroupBy(m => new { m.LeagueId, m.LeagueName, m.LeagueCountry, m.LeagueLogoUrl })
                .Select(g => new LeagueMatchesViewModel
                {
                    LeagueId = g.Key.LeagueId,
                    LeagueName = g.Key.LeagueName,
                    Country = g.Key.LeagueCountry,
                    LogoUrl = g.Key.LeagueLogoUrl,
                    Matches = g.OrderBy(m => m.MatchDate).ToList()
                })
                .ToList();

            var viewModel = new MatchIndexViewModel
            {
                PopularLeagues = leagues,
                LeagueGroups = leagueGroups,
                CurrentFilter = filter,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        private async Task<List<ResultMatchDto>> GetMatchesByStatus(HttpClient client, string status)
        {
            var response = await client.GetAsync($"api/Match/status/{status}");
            return await response.Content.ReadFromJsonAsync<List<ResultMatchDto>>() ?? new();
        }
    }
}