using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;
using System.Linq;

namespace Repositories.Repositories
{
    public class FilmRatingRepository : IFilmRatingRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<FilmRating> _filmRatingDataSet;

        public FilmRatingRepository(DbContext context)
        {
            _context = context;
            _filmRatingDataSet = context.Set<FilmRating>();
        }

        public async Task<double?> GetPersonalRatingAsync(long filmId, string userId)
        {
            var record = await _filmRatingDataSet.FirstOrDefaultAsync(p => p.FilmId == filmId && p.UserId == userId);

            return record?.Rating;
        }

        public double? GetTotalRating(long filmId)
        {
            var records = _filmRatingDataSet
                .Where(p => p.FilmId == filmId);

            if (records.Count() == 0)
                return null;

            return records.Average(p => p.Rating);
        }

        public async Task AddRatingAsync(long filmId, string userId, double rating)
        {
            var filmRating = new FilmRating
            {
                FilmId = filmId,
                Rating = rating,
                UserId = userId
            };

            _filmRatingDataSet.Add(filmRating);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(long filmId, string userId, double rating)
        {
            var filmRating = await _filmRatingDataSet.FirstOrDefaultAsync(p => p.FilmId == filmId && p.UserId == userId);
            filmRating.Rating = rating;

            _context.Entry(filmRating).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
