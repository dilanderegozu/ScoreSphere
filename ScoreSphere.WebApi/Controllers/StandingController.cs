using Microsoft.AspNetCore.Mvc;
using ScoreSphere.BusinessLayer.Abstract;

namespace ScoreSphere.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingController : ControllerBase
    {
        private readonly IStandingService _standingService;

        public StandingController(IStandingService standingService)
        {
            _standingService = standingService;
        }

        [HttpGet("season/{seasonId}")]
        public async Task<IActionResult> GetBySeason(int seasonId)
        {
            var standings = await _standingService.GetStandingsBySeasonAsync(seasonId);
            return Ok(standings);
        }
    }
}