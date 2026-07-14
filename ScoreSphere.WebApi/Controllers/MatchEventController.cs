using Microsoft.AspNetCore.Mvc;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DtoLayer.MatchEventDtos;

namespace ScoreSphere.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchEventController : ControllerBase
    {
        private readonly IMatchEventService _matchEventService;

        public MatchEventController(IMatchEventService matchEventService)
        {
            _matchEventService = matchEventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _matchEventService.TGetListAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var matchEvent = await _matchEventService.TGetByIdAsync(id);
            if (matchEvent == null)
                return NotFound(new { message = "Olay bulunamadı." });

            return Ok(matchEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMatchEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _matchEventService.TInsertAsync(dto);
            return Ok(new { message = "Olay eklendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var matchEvent = await _matchEventService.TGetByIdAsync(id);
            if (matchEvent == null)
                return NotFound(new { message = "Olay bulunamadı." });

            await _matchEventService.TDeleteAsync(id);
            return Ok(new { message = "Olay silindi." });
        }
    }
}