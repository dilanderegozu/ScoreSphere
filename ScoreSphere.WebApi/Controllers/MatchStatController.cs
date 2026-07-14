using Microsoft.AspNetCore.Mvc;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DtoLayer.MatchStatDtos;

namespace ScoreSphere.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchStatController : ControllerBase
    {
        private readonly IMatchStatService _matchStatService;

        public MatchStatController(IMatchStatService matchStatService)
        {
            _matchStatService = matchStatService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stat = await _matchStatService.TGetByIdAsync(id);
            if (stat == null)
                return NotFound(new { message = "İstatistik bulunamadı." });

            return Ok(stat);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMatchStatDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _matchStatService.TUpdateAsync(dto);
            return Ok(new { message = "İstatistik güncellendi." });
        }
    }
}