using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Film> _filmDataSet;

        public FilmRepository(DbContext context)
        {
            _context = context;
            _filmDataSet = context.Set<Film>();
        }

        public async Task<Film> GetFilmAsync(long filmId)
        {
            return await _filmDataSet.FirstOrDefaultAsync(p => p.Id == filmId);
        }

        public async Task<Film[]> GetFilmsAsync()
        {
            return await _filmDataSet.ToArrayAsync();
        }
    }
}
