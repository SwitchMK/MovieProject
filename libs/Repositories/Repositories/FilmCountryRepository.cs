using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;
using System.Linq;

namespace Repositories.Repositories
{
    public class FilmCountryRepository : IFilmCountryRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<FilmCountry> _filmCountryDataSet;

        public FilmCountryRepository(DbContext context)
        {
            _context = context;
            _filmCountryDataSet = context.Set<FilmCountry>();
        }

        public async Task<FilmCountry[]> GetFilmCountriesAsync(long filmId)
        {
            return await _filmCountryDataSet
                .Where(fc => fc.FilmId == filmId)
                .Include(fc => fc.Country)
                .ToArrayAsync();
        }
    }
}
