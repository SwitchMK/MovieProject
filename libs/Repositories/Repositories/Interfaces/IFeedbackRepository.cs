using Entities.Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        Task SaveFeedbackAsync(Feedback feedback);
        Task<Feedback[]> GetFeedbacksAsync(long filmId);
    }
}
