using Entities.Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IFilmCountryRepository
    {
        Task<FilmCountry[]> GetFilmCountriesAsync(long filmId);
    }
}
