using Entities.Models.Requests;
using Entities.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieProject.Services.Interfaces
{
    public interface IScreeningManagementService
    {
        Task<IEnumerable<FilmTheatreResponse>> GetTheatresAsync(TheatreRequest theatreRequest);
        Task<IEnumerable<FilmListItemResponse>> GetFilmsInTheatreAsync(FilmsInTheatreRequest filmsInTheatreRequest);
        Task<UpdateFilmTheatreResponse> SubmitTheatreInformationAsync(UpdateFilmTheatreRequest request);
        Task<UpdateFilmTheatreResponse> ExportToXmlFileAsync(ExportToFileRequest request);
        Task<UpdateFilmTheatreResponse> ImportFromXmlFileAsync(string path);
    }
}
