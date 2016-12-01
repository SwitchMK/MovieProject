using Entities.Entities;
using Entities.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IFilmTheatreRepository
    {
        Task<FilmTheatre[]> GetFilmTheatresExceptAsync(long filmId, long theatreId);
        Task<FilmTheatre[]> GetFilmTheatresAsync(long theatreId);
        Task<FilmTheatre[]> GetFilmTheatresAsync(TheatreRequest theatreRequest);
        Task<FilmTheatre[]> GetFilmTheatresAsync(FilmsInTheatreRequest filmsInTheatreRequest);
        Task<FilmTheatre> GetFilmTheatreAsync(long filmId, long theatreId);
        Task<FilmTheatre[]> GetFilmTheatresAsync(long? filmId = null, long? theatreId = null);
        Task AddFilmTheatreAsync(FilmTheatre filmTheatre);
        Task UpdateFilmTheatreAsync(FilmTheatre filmTheatre);
        Task AddFilmTheatresAsync(IEnumerable<FilmTheatre> filmTheatres);
    }
}
