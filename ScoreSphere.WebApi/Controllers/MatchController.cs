using Microsoft.AspNetCore.Mvc;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DtoLayer.MatchDtos;

namespace ScoreSphere.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matches = await _matchService.TGetListAsync();
            return Ok(matches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var match = await _matchService.TGetByIdAsync(id);
            if (match == null)
                return NotFound(new { message = "Maç bulunamadı." });

            return Ok(match);
        }

        [HttpGet("season/{seasonId}/week/{week}")]
        public async Task<IActionResult> GetByWeek(int seasonId, int week)
        {
            var matches = await _matchService.GetMatchesByWeekAsync(seasonId, week);
            return Ok(matches);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            try
            {
                var matches = await _matchService.GetMatchesByStatusAsync(status);
                return Ok(matches);
            }
            catch (ArgumentException)
            {
                return BadRequest(new { message = "Geçersiz durum değeri. Geçerli değerler: Upcoming, Live, HalfTime, Finished, Postponed, Cancelled" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMatchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _matchService.TInsertAsync(dto);
            return Ok(new { message = "Maç eklendi." });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMatchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _matchService.TUpdateAsync(dto);
            return Ok(new { message = "Maç güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var match = await _matchService.TGetByIdAsync(id);
            if (match == null)
                return NotFound(new { message = "Maç bulunamadı." });

            await _matchService.TDeleteAsync(id);
            return Ok(new { message = "Maç silindi." });
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var detail = await _matchService.GetMatchDetailAsync(id);
            if (detail == null)
                return NotFound(new { message = "Maç bulunamadı." });

            return Ok(detail);
        }
    }
}