using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models.Responses;
using MovieProject.Services.Interfaces;
using Repositories.Repositories.Interfaces;
using System.Linq;

namespace MovieProject.Services
{
    public class PeopleManagementService : IPeopleManagementService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IFilmPeopleRepository _filmPeopleRepository;
        private readonly IPeopleCareerRepository _peopleCareerRepository;

        public PeopleManagementService(
            IPeopleRepository peopleRepository,
            IFilmPeopleRepository filmPeopleRepository,
            IPeopleCareerRepository peopleCareerRepository)
        {
            _peopleRepository = peopleRepository;
            _filmPeopleRepository = filmPeopleRepository;
            _peopleCareerRepository = peopleCareerRepository;
        }

        public async Task<IEnumerable<PeopleListItemResponse>> GetPeopleAsync()
        {
            var responses = await _peopleRepository.GetPeopleAsync();

            return responses.Select(p => new PeopleListItemResponse
            {
                Id = p.Id,
                DateOfBirth = p.DateOfBirth,
                ShortName = p.ShortName,
                SmallPicturePath = p.SmallPicturePath
            });
        }

        public async Task<FullPeopleDetailsResponse> GetPersonDetailsAsync(long personId)
        {
            var person = await _peopleRepository.GetPersonAsync(personId);

            if (person == null)
                return null;

            var films = await _filmPeopleRepository.GetFilmPeopleAsync(peopleId: personId);

            var careers = await _peopleCareerRepository.GetPeopleCareersAsync(personId);

            return new FullPeopleDetailsResponse
            {
                Id = person.Id,
                DateOfBirth = person.DateOfBirth,
                FullName = person.FullName,
                ShortName = person.ShortName,
                Country = person.Country.Name,
                Careers = careers.Select(p => p.Career.Title),
                Films = films.Select(p => new SmallMovieResponse
                {
                    Id = p.Film.Id,
                    Title = p.Film.Title,
                    YearOfRelease = p.Film.YearOfRelease
                }),
                PicturePath = person.PicturePath,
                SmallPicturePath = person.SmallPicturePath,
                Biography = person.Biography
            };
        }
    }
}
