using Entities.Models.Requests;
using Entities.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Models;
using MovieProject.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieProject.Controllers.WebApi
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]/[action]")]
    public class FilmController : Controller
    {
        private readonly IFilmManagementService _filmManagementService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FilmController(
            IFilmManagementService filmManagementService,
            UserManager<ApplicationUser> userManager)
        {
            _filmManagementService = filmManagementService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<FilmListItemResponse>> GetFilms()
        {
            var userId = _userManager.GetUserId(User);

            return await _filmManagementService.GetFilmsAsync(userId);
        }

        [HttpPost]
        public async Task<FullFilmDetailsResponse> GetFilmDetails([FromBody] long filmId)
        {
            var userId = _userManager.GetUserId(User);

            return await _filmManagementService.GetFilmDetailsAsync(filmId, userId);
        }

        [HttpPost]
        public async Task<FullFilmDetailsResponse> RateMovie([FromBody] RateRequest rateRequest)
        {
            var userId = _userManager.GetUserId(User);

            return await _filmManagementService.RateMovieAsync(rateRequest, userId);
        }

        [HttpPost]
        public async Task<IEnumerable<FeedbackResponse>> LeaveFeedback([FromBody] FeedbackRequest feedbackRequest)
        {
            var userId = _userManager.GetUserId(User);

            return await _filmManagementService.LeaveFeedbackAsync(feedbackRequest, userId);
        }

        [HttpPost]
        public async Task<IEnumerable<FeedbackResponse>> GetFeedbacks([FromBody] long filmId)
        {
            return await _filmManagementService.GetFeedbacksAsync(filmId);
        }
    }
}
