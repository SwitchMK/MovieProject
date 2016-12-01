using Entities.Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IFilmRepository
    {
        Task<Film[]> GetFilmsAsync();
        Task<Film> GetFilmAsync(long filmId);
    }
}
