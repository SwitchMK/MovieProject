using Entities.Models.Requests;
using Entities.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Services.Interfaces;
using Repositories.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.Controllers.WebApi
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]/[action]")]
    public class ScreeningController : Controller
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ITheatreRepository _theatreRepository;
        private readonly IScreeningManagementService _screeningManagementService;

        public ScreeningController(
            IFilmRepository filmRepository,
            ITheatreRepository theatreRepository,
            IScreeningManagementService screeningManagementService)
        {
            _filmRepository = filmRepository;
            _theatreRepository = theatreRepository;
            _screeningManagementService = screeningManagementService;
        }

        [HttpGet]
        public async Task<IEnumerable<FilmListItemResponse>> GetFilms()
        {
            var films = await _filmRepository.GetFilmsAsync();

            return films.Select(f => new FilmListItemResponse
            {
                Id = f.Id,
                Title = f.Title,
                YearOfRelease = f.YearOfRelease
            });
        }

        [HttpPost]
        public async Task<IEnumerable<FilmTheatreResponse>> GetFilmTheatres([FromBody] TheatreRequest theatreRequest)
        {
            return await _screeningManagementService.GetTheatresAsync(theatreRequest);
        }

        [HttpGet]
        public async Task<IEnumerable<TheatreResponse>> GetTheatres()
        {
            var records = await _theatreRepository.GetTheatresAsync();

            return records.Select(p => new TheatreResponse
            {
                Id = p.Id,
                Title = p.Title
            });
        }

        [HttpPost]
        public async Task<IEnumerable<FilmListItemResponse>> GetFilmsInTheatre([FromBody] FilmsInTheatreRequest filmsInTheatreRequest)
        {
            return await _screeningManagementService.GetFilmsInTheatreAsync(filmsInTheatreRequest);
        }

        [HttpPost]
        public async Task<UpdateFilmTheatreResponse> SubmitTheatreInformation([FromBody] UpdateFilmTheatreRequest request)
        {
            return await _screeningManagementService.SubmitTheatreInformationAsync(request);
        }

        [HttpPost]
        public async Task<UpdateFilmTheatreResponse> ExportToXmlFile([FromBody] ExportToFileRequest request)
        {
            return await _screeningManagementService.ExportToXmlFileAsync(request);
        }

        [HttpPost]
        public async Task<UpdateFilmTheatreResponse> ImportFromXmlFile([FromBody] ImportFromFileRequest request)
        {
            return await _screeningManagementService.ImportFromXmlFileAsync(request.Path);
        }
    }
}
