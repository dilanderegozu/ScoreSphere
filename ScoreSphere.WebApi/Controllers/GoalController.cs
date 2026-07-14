using Microsoft.AspNetCore.Mvc;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DtoLayer.GoalDtos;

namespace ScoreSphere.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var goals = await _goalService.TGetListAsync();
            return Ok(goals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var goal = await _goalService.TGetByIdAsync(id);
            if (goal == null)
                return NotFound(new { message = "Gol bulunamadı." });

            return Ok(goal);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGoalDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _goalService.TInsertAsync(dto);
            return Ok(new { message = "Gol eklendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var goal = await _goalService.TGetByIdAsync(id);
            if (goal == null)
                return NotFound(new { message = "Gol bulunamadı." });

            await _goalService.TDeleteAsync(id);
            return Ok(new { message = "Gol silindi." });
        }
    }
}