using Microsoft.EntityFrameworkCore;

namespace MVAPI.Models
{
    public class AppDbcontext : DbContext 
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options): base(options) 
        {
            
        }

        //

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }
    }
}
