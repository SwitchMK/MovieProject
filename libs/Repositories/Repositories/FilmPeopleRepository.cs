using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;
using System.Linq;

namespace Repositories.Repositories
{
    public class FilmPeopleRepository : IFilmPeopleRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<FilmPeople> _filmPeopleDataSet;

        public FilmPeopleRepository(DbContext context)
        {
            _context = context;
            _filmPeopleDataSet = context.Set<FilmPeople>();
        }

        public async Task<FilmPeople[]> GetFilmPeopleAsync(long? filmId = null, long? personId = null)
        {
            return await _filmPeopleDataSet
                .Where(p => 
                    (filmId == null || p.FilmId == filmId) && 
                    (personId == null || p.PeopleId == personId))
                .Include(p => p.People)
                .Include(p => p.Career)
                .Include(p => p.Film)
                .ToArrayAsync();
        }
    }
}
