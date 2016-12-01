using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models.Requests;
using Entities.Models.Responses;
using MovieProject.Services.Interfaces;
using Repositories.Repositories.Interfaces;
using System.Linq;
using Entities.Other;
using Entities.Entities;
using System.Xml;
using System;

namespace MovieProject.Services
{
    public class ScreeningManagementService : IScreeningManagementService
    {
        private readonly IFilmTheatreRepository _filmTheatreRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly ITheatreRepository _theatreRepository;

        private readonly string _notNullMessage = "Submitting failed! Values cannot be null, please fill empty fields.";
        private readonly string _successAddingMessage = "Successfully submitted! Entity has been successfully added.";
        private readonly string _isMovieAlreadyOnScreen = "Submitting failed! Selected movie is already on screen.";
        private readonly string _successUpdatingMessage = "Successfully submitted! Entity has been successfully updated.";
        private readonly string _boxOfficeIsExceeded = "Submitting failed! Box office has exceeded total box office.";
        private readonly string _successExportToXml = "Successfully exported data to XML file.";
        private readonly string _unknownDataMessage = "unknown";

        public ScreeningManagementService(
            IFilmTheatreRepository filmTheatreRepository,
            IFilmRepository filmRepository,
            ITheatreRepository theatreRepository)
        {
            _filmTheatreRepository = filmTheatreRepository;
            _filmRepository = filmRepository;
            _theatreRepository = theatreRepository;
        }

        public async Task<IEnumerable<FilmListItemResponse>> GetFilmsInTheatreAsync(FilmsInTheatreRequest filmsInTheatreRequest)
        {
            var records = await _filmTheatreRepository.GetFilmTheatresAsync(filmsInTheatreRequest);

            return records
                .GroupBy(p => new { p.Film.Id, p.Film.Title })
                .Select(p => new FilmListItemResponse
                {
                    Id = p.Key.Id,
                    Title = p.Key.Title
                });
        }

        public async Task<IEnumerable<FilmTheatreResponse>> GetTheatresAsync(TheatreRequest theatreRequest)
        {
            var records = await _filmTheatreRepository.GetFilmTheatresAsync(theatreRequest);

            if (theatreRequest.FilmId == null)
                return null;

            var totalBoxOffice = records.Sum(p => p.BoxOfficePerMovie);

            return records
                .Select(p => new FilmTheatreResponse
                {
                    Title = p.Theatre.Title,
                    Country = p.Theatre.Country.Name,
                    PercentageOfBoxOffice = (double?)(p.BoxOfficePerMovie / totalBoxOffice) * 100
                });
        }

        public async Task<UpdateFilmTheatreResponse> SubmitTheatreInformationAsync(UpdateFilmTheatreRequest request)
        {
            return await ValidateAndSubmitAsync(request);
        }

        private async Task<UpdateFilmTheatreResponse> ValidateAndSubmitAsync(UpdateFilmTheatreRequest request)
        {
            if (IsNotAllPropertiesInitialized(request))
                return GetUpdateFilmThatreResponse(_notNullMessage, SubmitStatus.Error);

            var filmTheatre = await _filmTheatreRepository.GetFilmTheatreAsync(request.FilmId.Value, request.TheatreId.Value);

            if (!(await IsBoxOfficeCorrect(request)))
                return GetUpdateFilmThatreResponse(_boxOfficeIsExceeded, SubmitStatus.Error);

            if (filmTheatre == null)
            {
                var newFilmTheatre = GetFilmTheatreEntity(request);
                await _filmTheatreRepository.AddFilmTheatreAsync(newFilmTheatre);

                return GetUpdateFilmThatreResponse(_successAddingMessage, SubmitStatus.Success);
            }

            var filmTheatres = await _filmTheatreRepository.GetFilmTheatresAsync(request.TheatreId.Value);

            if (IsMovieAlreadyOnScreen(request, filmTheatres))
                return GetUpdateFilmThatreResponse(_isMovieAlreadyOnScreen, SubmitStatus.Error);

            UpdateFilmTheatre(request, filmTheatre);

            await _filmTheatreRepository.UpdateFilmTheatreAsync(filmTheatre);

            return GetUpdateFilmThatreResponse(_successUpdatingMessage, SubmitStatus.Success);
        }

        private async Task<bool> IsBoxOfficeCorrect(UpdateFilmTheatreRequest request)
        {
            var film = await _filmRepository.GetFilmAsync(request.FilmId.Value);

            var filmTheatres = await _filmTheatreRepository.GetFilmTheatresExceptAsync(request.FilmId.Value, request.TheatreId.Value);

            var totalBoxOffice = filmTheatres.Sum(p => p.BoxOfficePerMovie) + request.BoxOffice;

            return totalBoxOffice <= film.BoxOffice;
        }

        private bool IsMovieAlreadyOnScreen(UpdateFilmTheatreRequest request, FilmTheatre[] filmTheatres)
        {
            return filmTheatres.Any(p => (p.StartDistributionDate < request.EndDistributionDate &&
                request.StartDistributionDate < p.EndDistributionDate) && (request.FilmId == p.FilmId) &&
                (request.TheatreId == p.TheatreId));
        }

        private bool IsNotAllPropertiesInitialized(UpdateFilmTheatreRequest request)
        {
            if (request.AmountOfPeople == null || request.BoxOffice == null ||
                request.StartDistributionDate == null || request.EndDistributionDate == null ||
                request.FilmId == null || request.TheatreId == null)
                return true;

            return false;
        }

        private FilmTheatre GetFilmTheatreEntity(UpdateFilmTheatreRequest request)
        {
            return new FilmTheatre
            {
                FilmId = request.FilmId.Value,
                TheatreId = request.TheatreId.Value,
                AmountOfPeople = request.AmountOfPeople.Value,
                BoxOfficePerMovie = request.BoxOffice.Value,
                StartDistributionDate = request.StartDistributionDate.Value,
                EndDistributionDate = request.EndDistributionDate.Value
            };
        }

        private void UpdateFilmTheatre(UpdateFilmTheatreRequest request, FilmTheatre filmTheatre)
        {
            filmTheatre.StartDistributionDate = request.StartDistributionDate.Value;
            filmTheatre.EndDistributionDate = request.EndDistributionDate.Value;
            filmTheatre.AmountOfPeople = request.AmountOfPeople.Value;
            filmTheatre.BoxOfficePerMovie = request.BoxOffice.Value;
        }

        private UpdateFilmTheatreResponse GetUpdateFilmThatreResponse(string message, SubmitStatus status)
        {
            return new UpdateFilmTheatreResponse
            {
                Message = message,
                Status = status.ToString()
            };
        }

        public async Task<UpdateFilmTheatreResponse> ExportToXmlFileAsync(ExportToFileRequest request)
        {
            await SaveStructuredXML(request);

            return GetUpdateFilmThatreResponse(_successExportToXml, SubmitStatus.Success);
        }

        private async Task SaveStructuredXML(ExportToFileRequest request)
        {
            var films = await _filmRepository.GetFilmsAsync();
            var theatres = await _theatreRepository.GetTheatresAsync();

            var filmIndentifiers = new List<long>();
            var theatreIndentifiers = new List<long>();

            if (request.UpdateFilmTheatreRequest.FilmId != null)
                filmIndentifiers.Add(request.UpdateFilmTheatreRequest.FilmId.Value);
            else
                filmIndentifiers.AddRange(films.Select(p => p.Id));

            if (request.UpdateFilmTheatreRequest.TheatreId != null)
                theatreIndentifiers.Add(request.UpdateFilmTheatreRequest.TheatreId.Value);
            else
                theatreIndentifiers.AddRange(theatres.Select(p => p.Id));

            SaveToXML(filmIndentifiers, theatreIndentifiers, request);
        }

        private void SaveToXML(
            List<long> filmIndentifiers,
            List<long> theatreIndentifiers,
            ExportToFileRequest request)
        {
            var doc = new XmlDocument();

            doc.LoadXml("<container></container>");

            foreach (var film in filmIndentifiers)
            {
                foreach (var theatre in theatreIndentifiers)
                {
                    var updateRequestItem = doc.CreateElement("updateRequest");
                    doc.DocumentElement.AppendChild(updateRequestItem);

                    foreach (var property in request.UpdateFilmTheatreRequest.GetType().GetProperties())
                    {
                        var propertyName = property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1);
                        var prop = doc.CreateElement(propertyName);

                        if (propertyName == "filmId")
                            prop.InnerText = film.ToString();
                        else if (propertyName == "theatreId")
                            prop.InnerText = theatre.ToString();
                        else
                            prop.InnerText = property.GetValue(request.UpdateFilmTheatreRequest)?.ToString() ?? _unknownDataMessage;

                        updateRequestItem.AppendChild(prop);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            doc.Save(request.Path);
        }

        public async Task<UpdateFilmTheatreResponse> ImportFromXmlFile(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);

            var items = new List<Dictionary<string, string>>();

            foreach (var node in doc.DocumentElement.ChildNodes)
            {
                var xmlData = new Dictionary<string, string>();

                foreach (var innerNode in (node as XmlNode).ChildNodes)
                {
                    xmlData.Add((innerNode as XmlNode).Name, (innerNode as XmlNode).InnerText);
                }

                items.Add(xmlData);
            }

            var responses = new List<ImportFromFileResponse>();

            items.ForEach(item => responses.Add(ValidateDataFromXml(item)));

            var readyForDb = await GetReadyForDbData(responses);

            await _filmTheatreRepository.AddFilmTheatresAsync(GetFilmTheatres(readyForDb));

            return GetUpdateFilmThatreResponse($"Submitted {readyForDb.Count()} records from {items.Count()}.", SubmitStatus.Info);
        }

        private async Task<IEnumerable<ImportFromFileResponse>> GetReadyForDbData(List<ImportFromFileResponse> responses)
        {
            var uniqueResponses = responses.Where(r => !responses.Any(ar => r.FilmId != ar.FilmId && r.TheatreId == ar.TheatreId &&
                (r.StartDistributionDate <= ar.EndDistributionDate && ar.StartDistributionDate <= r.EndDistributionDate)));

            var filmTheatres = await _filmTheatreRepository.GetFilmTheatresAsync();

            return uniqueResponses.Where(r => !filmTheatres.Any(ft => (r.StartDistributionDate <= ft.EndDistributionDate &&
                ft.StartDistributionDate <= r.EndDistributionDate)));
        }

        private IEnumerable<FilmTheatre> GetFilmTheatres(IEnumerable<ImportFromFileResponse> responses)
        {
            return responses.Select(item => new FilmTheatre
            {
                FilmId = item.FilmId.Value,
                TheatreId = item.TheatreId.Value,
                AmountOfPeople = item.AmountOfPeople.Value,
                BoxOfficePerMovie = item.BoxOffice.Value,
                StartDistributionDate = item.StartDistributionDate.Value,
                EndDistributionDate = item.EndDistributionDate.Value
            });
        }

        private ImportFromFileResponse ValidateDataFromXml(Dictionary<string, string> xmlData)
        {
            var response = new ImportFromFileResponse();

            long filmId, theatreId;
            DateTime startDate, endDate;
            decimal boxOffice;
            int amountOfPeople;

            long.TryParse(xmlData
                .FirstOrDefault(p => p.Key.ToLower() == nameof(response.FilmId).ToLower())
                .Value, out filmId);

            long.TryParse(xmlData
                .FirstOrDefault(p => p.Key.ToLower() == nameof(response.TheatreId).ToLower())
                .Value, out theatreId);

            int.TryParse(xmlData
                .FirstOrDefault(p => p.Key.ToLower() == nameof(response.AmountOfPeople).ToLower())
                .Value, out amountOfPeople);

            decimal.TryParse(xmlData
                .FirstOrDefault(p => p.Key.ToLower() == nameof(response.BoxOffice).ToLower())
                .Value, out boxOffice);

            DateTime.TryParse(xmlData
                .FirstOrDefault(p => p.Key.ToLower() == nameof(response.EndDistributionDate).ToLower())
                .Value, out endDate);

            DateTime.TryParse(xmlData
                .FirstOrDefault(p => p.Key.ToLower() == nameof(response.StartDistributionDate).ToLower())
                .Value, out startDate);

            response.AmountOfPeople = amountOfPeople;
            response.BoxOffice = boxOffice;
            response.FilmId = filmId;
            response.TheatreId = theatreId;
            response.StartDistributionDate = startDate;
            response.EndDistributionDate = endDate;

            return response;
        }
    }
}

