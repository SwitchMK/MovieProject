using System.Threading.Tasks;
using Entities.Entities;
using Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Entities.Models.Requests;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Repositories.Repositories
{
    public class FilmTheatreRepository : IFilmTheatreRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<FilmTheatre> _filmTheatreDataSet;

        public FilmTheatreRepository(DbContext context)
        {
            _context = context;
            _filmTheatreDataSet = context.Set<FilmTheatre>();        
        }

        public async Task<FilmTheatre> GetFilmTheatreAsync(long filmId, long theatreId)
        {
            return await _filmTheatreDataSet.FirstOrDefaultAsync(p => p.FilmId == filmId && p.TheatreId == theatreId);
        }

        public async Task<FilmTheatre[]> GetFilmTheatresAsync(long theatreId)
        {
            return await _filmTheatreDataSet
                .Where(p => p.TheatreId == theatreId)
                .Include(p => p.Theatre)
                .ToArrayAsync();
        }

        public async Task<FilmTheatre[]> GetFilmTheatresAsync(TheatreRequest theatreRequest)
        {
            return await _filmTheatreDataSet
                .Where(t => (theatreRequest.FilmId == null || t.FilmId == theatreRequest.FilmId) &&
                    (theatreRequest.EndDate == null || t.StartDistributionDate < theatreRequest.EndDate) &&
                    (theatreRequest.StartDate == null || theatreRequest.StartDate < t.EndDistributionDate))
                .Include(t => t.Theatre)
                .Include(t => t.Theatre.Country)
                .ToArrayAsync();
        }

        public async Task<FilmTheatre[]> GetFilmTheatresAsync(FilmsInTheatreRequest filmsInTheatreRequest)
        {
            return await _filmTheatreDataSet
                .Where(t => (filmsInTheatreRequest.Day == null || (filmsInTheatreRequest.Day >= t.StartDistributionDate
                    && filmsInTheatreRequest.Day <= t.EndDistributionDate)) && (filmsInTheatreRequest.TheatreId == null ||
                    filmsInTheatreRequest.TheatreId == t.TheatreId))
                .Include(t => t.Film)
                .ToArrayAsync();
        }

        public async Task AddFilmTheatreAsync(FilmTheatre filmTheatre)
        {
            _filmTheatreDataSet.Add(filmTheatre);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateFilmTheatreAsync(FilmTheatre filmTheatre)
        {
            _context.Entry(filmTheatre).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<FilmTheatre[]> GetFilmTheatresExceptAsync(long filmId, long theatreId)
        {
            return await _filmTheatreDataSet.Where(p => p.TheatreId != theatreId && p.FilmId == filmId).ToArrayAsync();
        }

        public async Task<FilmTheatre[]> GetFilmTheatresAsync(long? filmId = null, long? theatreId = null)
        {
            return await _filmTheatreDataSet.Where(p => (filmId == null || p.FilmId == filmId) &&
                (theatreId == null || p.TheatreId == theatreId)).ToArrayAsync();
        }

        public async Task AddFilmTheatresAsync(IEnumerable<FilmTheatre> filmTheatres)
        {
            _filmTheatreDataSet.AddRange(filmTheatres);

            await _context.SaveChangesAsync();
        }
    }
}
