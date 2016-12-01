using Entities.Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IFilmPeopleRepository
    {
        Task<FilmPeople[]> GetFilmPeopleAsync(long? filmId = null, long? peopleId = null); 
    }
}
