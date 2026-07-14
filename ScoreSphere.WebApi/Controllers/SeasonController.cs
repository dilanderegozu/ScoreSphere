using Microsoft.AspNetCore.Mvc;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DtoLayer.SeasonDtos;

namespace ScoreSphere.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var seasons = await _seasonService.TGetListAsync();
            return Ok(seasons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var season = await _seasonService.TGetByIdAsync(id);
            if (season == null)
                return NotFound(new { message = "Sezon bulunamadı." });

            return Ok(season);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSeasonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _seasonService.TInsertAsync(dto);
            return Ok(new { message = "Sezon eklendi." });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSeasonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _seasonService.TUpdateAsync(dto);
            return Ok(new { message = "Sezon güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var season = await _seasonService.TGetByIdAsync(id);
            if (season == null)
                return NotFound(new { message = "Sezon bulunamadı." });

            await _seasonService.TDeleteAsync(id);
            return Ok(new { message = "Sezon silindi." });
        }
    }
}