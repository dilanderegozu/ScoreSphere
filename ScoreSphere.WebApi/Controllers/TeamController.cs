using Microsoft.AspNetCore.Mvc;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DtoLayer.TeamDtos;

namespace ScoreSphere.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _teamService.TGetListAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var team = await _teamService.TGetByIdAsync(id);

            if (team == null)
                return NotFound(new { message = "Takım bulunamadı." });

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamDto createTeamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _teamService.TInsertAsync(createTeamDto);
            return Ok(new { message = "Takım eklendi." });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTeamDto updateTeamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _teamService.TUpdateAsync(updateTeamDto);
            return Ok(new { message = "Takım güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _teamService.TGetByIdAsync(id);

            if (team == null)
                return NotFound(new { message = "Takım bulunamadı." });

            await _teamService.TDeleteAsync(id);
            return Ok(new { message = "Takım silindi." });
        }
    }
}