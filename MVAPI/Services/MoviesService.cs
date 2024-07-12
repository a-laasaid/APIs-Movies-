
using Microsoft.EntityFrameworkCore;


namespace MVAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly AppDbcontext _Context;

        public MoviesService(AppDbcontext context)
        {
            _Context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _Context.AddAsync(movie);
            _Context.SaveChanges();

            return movie;
        }

        public async Task<Movie> Delete(Movie movie)
        {
            _Context.Update(movie);
            _Context.SaveChanges();

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await _Context.Movies
                .Where(m=>m.GenreId == genreId || genreId ==0)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _Context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> Update(Movie movie)
        {
            _Context.Update(movie);
           _Context.SaveChanges();

            return movie;
        }
    }
}
