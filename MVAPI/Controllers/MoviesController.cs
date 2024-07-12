using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVAPI.Models;
using MVAPI.Services;


namespace MVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;


        private new List<string> _allowedExtenstions = new List<string> { ".jpg",".png"};

        public MoviesController(IMoviesService moviesService, IGenresService genresService , IMapper mapper)
        {
            _moviesService = moviesService;
            _genresService = genresService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.GetAll();

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }
        [HttpGet(template:"{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _moviesService.GetById(id) ;

            if (movie == null)
                return NotFound();


            var dto = _mapper.Map<MovieDetailsDto>(movie) ;
            return Ok(dto);
        }
        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _moviesService.GetAll(genreId);
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreatAsync([FromForm]Moviedto dto)
        {
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg image are allowed!");

            var isvalidGenre = await _genresService.IsvalidGenre(dto.GenreId);
            if (isvalidGenre) { return BadRequest("Invalid genre Id!"); }
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var movie = _mapper.Map<Movie>(dto);

            movie.Poster=dataStream.ToArray();

          _moviesService.Add(movie);

            return Ok(movie);
        }

        [HttpPut(template:"{id}")]
        public async Task<IActionResult> UpdateAsync(int id , [FromForm] Moviedto dto)
        {
            if (dto.Poster == null)
                return BadRequest("poster is required!");
            var movie = await _moviesService.GetById(id);
            if (movie == null) return NotFound($"No Movie was found with ID {id}");

            var isvalidGenre = await _genresService.IsvalidGenre(dto.GenreId);
            if (isvalidGenre) { return BadRequest("Invalid genre Id!"); }

            if(dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg image are allowed!");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster= dataStream.ToArray();
            }



            movie.Title = dto.Title;
            movie.Storyline = dto.Storyline;
            movie.Year = dto.Year;
            movie.GenreId = dto.GenreId;
            movie.Rate = dto.Rate;

                _moviesService.Update(movie);
                return Ok(movie);
        }
        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null) return NotFound($"No Movie was found with ID {id}"); 

           _moviesService.Delete(movie);

            return Ok(movie);
        }
    }
}
