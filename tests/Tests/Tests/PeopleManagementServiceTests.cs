using Moq;
using Xunit;
using Autofac.Extras.Moq;
using MovieProject.Services;
using Repositories.Repositories.Interfaces;
using System.Threading.Tasks;
using Entities.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tests
{
    public class PeopleManagementServiceTests
    {
        private AutoMock _autoMockContext { get; }

        public PeopleManagementServiceTests()
        {
            _autoMockContext = AutoMock.GetLoose();
        }

        [Fact]
        public async Task PeopleManagementService_GetPersonDetailsAsync()
        {
            var personId = 2;

            MockPeopleRepository_GetPersonAsync();
            MockFilmPeopleRepository();
            MockPeopleCareerRepository();

            var peopleManagementService = _autoMockContext.Create<PeopleManagementService>();
            var actual = await peopleManagementService.GetPersonDetailsAsync(personId);

            Assert.Equal(2, actual.Careers.Count());
            Assert.Equal(3, actual.Films.Count());
        }

        [Fact]
        public async Task PeopleManagementService_GetPersonDetailsAsync_VerifyNull()
        {
            var personId = 2;

            MockPeopleRepository_GetPersonAsync_Null();

            var peopleManagementService = _autoMockContext.Create<PeopleManagementService>();
            var actual = await peopleManagementService.GetPersonDetailsAsync(personId);

            Assert.Null(actual);
        }

        private void MockPeopleRepository_GetPersonAsync_Null()
        {
            _autoMockContext.Mock<IPeopleRepository>()
                .Setup(m => m.GetPersonAsync(It.IsAny<long>()))
                .ReturnsAsync(null);
        }

        private void MockPeopleRepository_GetPersonAsync()
        {
            _autoMockContext.Mock<IPeopleRepository>()
                .Setup(m => m.GetPersonAsync(It.IsAny<long>()))
                .ReturnsAsync(MockPeopleRequest());
        }

        private void MockFilmPeopleRepository()
        {
            _autoMockContext.Mock<IFilmPeopleRepository>()
               .Setup(m => m.GetFilmPeopleAsync(It.IsAny<long?>(), It.IsAny<long?>()))
               .ReturnsAsync(MockFilmPeopleRequest());
        }

        private void MockPeopleCareerRepository()
        {
            _autoMockContext.Mock<IPeopleCareerRepository>()
               .Setup(m => m.GetPeopleCareersAsync(It.IsAny<long>()))
               .ReturnsAsync(MockCareersRequest());
        }

        private People MockPeopleRequest()
        {
            return new People
            {
                Biography = "Foo",
                Country = new Country
                {
                    Name = "Foo"
                },
                FullName = "Foo",
                ShortName = "Foo",
                DateOfBirth = DateTime.Now,
                PicturePath = "Foo",
                SmallPicturePath = "Foo"
            };
        }

        private PeopleCareer[] MockCareersRequest()
        {
            return new List<PeopleCareer>
            {
                new PeopleCareer
                {
                    Career = new Career
                    {
                        Title = "Foo"
                    }
                },
                new PeopleCareer
                {
                    Career = new Career
                    {
                        Title = "Foo"
                    }
                }
            }
            .ToArray();
        }

        private FilmPeople[] MockFilmPeopleRequest()
        {
            return new List<FilmPeople>
            {
                new FilmPeople
                {
                    Film = new Film
                    {
                        Id = 1,
                        Title = "Foo",
                        YearOfRelease = DateTime.Now
                    }
                },
                new FilmPeople
                {
                    Film = new Film
                    {
                        Id = 2,
                        Title = "Foo",
                        YearOfRelease = DateTime.Now
                    }
                },
                new FilmPeople
                {
                    Film = new Film
                    {
                        Id = 3,
                        Title = "Foo",
                        YearOfRelease = DateTime.Now
                    }
                 }
            }
            .ToArray();
        }
    }
}
