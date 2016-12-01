using Entities.Models.Responses;
using MovieProject.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models.Requests;
using System;
using Repositories.Repositories.Interfaces;
using MovieProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Entities.Entities;

namespace MovieProject.Services
{
    public class FilmManagementService : IFilmManagementService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IFilmCountryRepository _filmCountryRepository;
        private readonly IFilmPeopleRepository _filmPeopleRepository;
        private readonly IFilmRatingRepository _filmRatingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDistributorRepository _distributorRepository;
        private readonly IFeedbackRepository _feedbaclRepository;

        long actorId = 1, actressId = 2, directorId = 5, producerId = 6, writerId = 8, composerId = 9;

        public FilmManagementService(
            IFilmRepository filmRepository,
            IFilmCountryRepository filmCountryRepository,
            IFilmPeopleRepository filmPeopleRepository,
            IFilmRatingRepository filmRatingRepository,
            UserManager<ApplicationUser> userManager,
            IDistributorRepository distributorRepository,
            IFeedbackRepository feedbaclRepository)
        {
            _filmRepository = filmRepository;
            _filmCountryRepository = filmCountryRepository;
            _filmPeopleRepository = filmPeopleRepository;
            _filmRatingRepository = filmRatingRepository;
            _userManager = userManager;
            _distributorRepository = distributorRepository;
            _feedbaclRepository = feedbaclRepository;
        }

        public async Task<IEnumerable<FeedbackResponse>> GetFeedbacksAsync(long filmId)
        {
            var feedbacks = await _feedbaclRepository.GetFeedbacksAsync(filmId);

            return feedbacks.Select(p => new FeedbackResponse
            {
                DateOfPublication = p.DateOfPublication,
                Text = p.Text,
                UserName = p.User.UserName
            });
        }

        public async Task<FullFilmDetailsResponse> GetFilmDetailsAsync(long filmId, string userId)
        {
            var film = await _filmRepository.GetFilmAsync(filmId);

            if (film == null)
                return null;

            var filmRating = _filmRatingRepository.GetTotalRating(filmId);
            var filmPersonalRating = await _filmRatingRepository.GetPersonalRatingAsync(filmId, userId);

            var countries = await _filmCountryRepository.GetFilmCountriesAsync(filmId);

            var distributor = await _distributorRepository.GetDistributorAsync(film.DistributorId);

            var people = await _filmPeopleRepository.GetFilmPeopleAsync(film.Id);

            return GetFullFilmDetailsResponse(film, filmRating, filmPersonalRating, people, distributor, countries);
        }

        public async Task<IEnumerable<FilmListItemResponse>> GetFilmsAsync(string userId)
        {
            var films = await _filmRepository.GetFilmsAsync();
            var filmResponses = new List<FilmListItemResponse>();

            foreach (var film in films)
            {
                var filmRating = _filmRatingRepository.GetTotalRating(film.Id);

                var filmPersonalRating = await _filmRatingRepository.GetPersonalRatingAsync(film.Id, userId);

                filmResponses.Add(GetFilmListItemResponse(film, filmRating, filmPersonalRating));
            }

            return filmResponses.OrderByDescending(p => p.TotalRating);
        }

        public async Task<IEnumerable<FeedbackResponse>> LeaveFeedbackAsync(FeedbackRequest feedbackRequest, string userId)
        {
            var feedback = new Feedback
            {
                FilmId = feedbackRequest.FilmId,
                Text = feedbackRequest.Message,
                UserId = userId,
                DateOfPublication = DateTime.Now
            };

            await _feedbaclRepository.SaveFeedbackAsync(feedback);

            return await GetFeedbacksAsync(feedbackRequest.FilmId);
        }

        public async Task<FullFilmDetailsResponse> RateMovieAsync(RateRequest rateRequest, string userId)
        {
            var personalRating = await _filmRatingRepository.GetPersonalRatingAsync(rateRequest.FilmId, userId);

            if (personalRating != null)
                await _filmRatingRepository.UpdateRatingAsync(rateRequest.FilmId, userId, rateRequest.Rating);
            else
                await _filmRatingRepository.AddRatingAsync(rateRequest.FilmId, userId, rateRequest.Rating);

            return await GetFilmDetailsAsync(rateRequest.FilmId, userId);
        }

        private FullFilmDetailsResponse GetFullFilmDetailsResponse(
            Film film, 
            double? filmRating,
            double? filmPersonalRating,
            FilmPeople[] people,
            Distributor distributor,
            FilmCountry[] countries)
        {
            return new FullFilmDetailsResponse
            {
                Title = film.Title,
                YearOfRelease = film.YearOfRelease,
                Id = film.Id,
                TotalRating = filmRating,
                PersonalRating = filmPersonalRating,
                PicturePath = film.PicturePath,
                BoxOffice = film.BoxOffice,
                Budget = film.Budget,
                Stars = people
                    .Where(p => p.Career.Id == actorId || p.Career.Id == actressId)
                    .Select(p => GetSmallPeopleResponse(p)),
                Directors = people
                    .Where(p => p.Career.Id == directorId)
                    .Select(p => GetSmallPeopleResponse(p)),
                Composers = people
                    .Where(p => p.Career.Id == composerId)
                    .Select(p => GetSmallPeopleResponse(p)),
                Producers = people
                    .Where(p => p.Career.Id == producerId)
                    .Select(p => GetSmallPeopleResponse(p)),
                Writers = people
                    .Where(p => p.Career.Id == writerId)
                    .Select(p => GetSmallPeopleResponse(p)),
                Countries = countries.Select(p => p.Country.Name),
                Distributor = distributor?.Title,
                Plot = film.Plot
            };
        }

        private SmallPeopleResponse GetSmallPeopleResponse(FilmPeople p)
        {
            return new SmallPeopleResponse
            {
                Id = p.People.Id,
                Name = p.People.ShortName
            };
        }

        private FilmListItemResponse GetFilmListItemResponse(Film film, double? filmRating, double? filmPersonalRating)
        {
            return new FilmListItemResponse
            {
                Title = film.Title,
                YearOfRelease = film.YearOfRelease,
                Id = film.Id,
                TotalRating = filmRating,
                PersonalRating = filmPersonalRating,
                SmallPicturePath = film.SmallPicturePath
            };
        }
    }
}
