using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IFilmRatingRepository
    {
        Task<double?> GetPersonalRatingAsync(long filmId, string userId);
        double? GetTotalRating(long filmId);
        Task AddRatingAsync(long filmId, string userId, double rating);
        Task UpdateRatingAsync(long filmId, string userId, double rating);
    }
}
