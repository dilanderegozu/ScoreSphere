using Microsoft.AspNetCore.Mvc;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DtoLayer.LeagueDtos;

namespace ScoreSphere.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leagues = await _leagueService.TGetListAsync();
            return Ok(leagues);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var league = await _leagueService.TGetByIdAsync(id);
            if (league == null)
                return NotFound(new { message = "Lig bulunamadı." });

            return Ok(league);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeagueDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _leagueService.TInsertAsync(dto);
            return Ok(new { message = "Lig eklendi." });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLeagueDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _leagueService.TUpdateAsync(dto);
            return Ok(new { message = "Lig güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var league = await _leagueService.TGetByIdAsync(id);
            if (league == null)
                return NotFound(new { message = "Lig bulunamadı." });

            await _leagueService.TDeleteAsync(id);
            return Ok(new { message = "Lig silindi." });
        }
    }
}