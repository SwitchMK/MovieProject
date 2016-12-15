using Entities.Models.Requests;
using Entities.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieProject.Services.Interfaces
{
    public interface IFilmManagementService
    {
        Task<IEnumerable<FilmListItemResponse>> GetFilmsAsync(string userId);
        Task<FullFilmDetailsResponse> GetFilmDetailsAsync(long filmId, string userId);
        Task<IEnumerable<FilmListItemResponse>> RateMovieAsync(RateRequest rateRequest, string userId);
        Task<IEnumerable<FeedbackResponse>> LeaveFeedbackAsync(FeedbackRequest feedbackRequest, string userId);
        Task<IEnumerable<FeedbackResponse>> GetFeedbacksAsync(long filmId);
    }
}
