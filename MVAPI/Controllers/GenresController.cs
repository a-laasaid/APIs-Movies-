using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVAPI.Services;

namespace MVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        //private readonly AppDbcontext _context;
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresService.GetAll();
            return Ok(genres);
        }

        [HttpPost]

        public async Task<IActionResult> CreatAsync(CreateGenreDTO dto)
        {
            //so we need class DTO to transcat Data to API 
            var genre = new Genre { Name = dto.Name };
            await _genresService.Add(genre);

            return Ok(genre);

        }


        [HttpPut("{Id}")]
        //Id , value
        //api/Genres/Id
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CreateGenreDTO dto)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was ound with Id:{id}");
        
            genre.Name = dto.Name;
            _genresService.Update(genre);

            return Ok(genre);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletAsync(byte id)
        {
            var genre = await _genresService.GetById(
                id);

            if (genre == null)
                return NotFound($"No genre was ound with Id:{id}");
            
           _genresService.Delete(genre);
            return Ok(genre);
        }


    }
}
