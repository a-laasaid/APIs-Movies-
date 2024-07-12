
using Microsoft.EntityFrameworkCore;

namespace MVAPI.Services
{
    public class GenresService : IGenresService
    {
        private readonly AppDbcontext _context;

        public GenresService(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.AddAsync(genre);
            _context.SaveChanges();

            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return genres;
        
        }

        public async Task<Genre> GetById(int id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<bool> IsvalidGenre(int id)
        {
            return _context.Genres.AnyAsync(g => g.Id == id);
        }

        public Genre Update(Genre genre)
        {
           _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }
    }
}
