using Moq;
using Xunit;
using Autofac.Extras.Moq;
using MovieProject.Services;
using System.Threading.Tasks;
using Entities.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using Repositories.Repositories.Interfaces;
using MovieProject.Models;
using Entities.Models.Requests;

namespace Tests
{
    public class FilmManagementServiceTests
    {
        private AutoMock _autoMockContext { get; }

        public FilmManagementServiceTests()
        {
            _autoMockContext = AutoMock.GetLoose();
        }

        [Fact]
        public async Task FilmManagementService_GetFilmsAsync_VerifyExecution()
        {
            var userId = "abcd";
            var expected = MockFilmsRequest().Length;

            MockFilmRepository_GetFilmsAsync();
            MockFilmRatingRepository();

            var filmManagementService = _autoMockContext.Create<FilmManagementService>();
            var actual = await filmManagementService.GetFilmsAsync(userId);

            Assert.Equal(expected, actual.Count());
        }

        [Fact]
        public async Task FilmManagementService_GetFeedbacksAsync_VerifyExecution()
        {
            var filmId = 2;
            var expected = MockFeedbacksRequest().Length;

            MockFeedbackRepository_GetFeedbacksAsync();

            var filmManagementService = _autoMockContext.Create<FilmManagementService>();
            var actual = await filmManagementService.GetFeedbacksAsync(filmId);

            Assert.Equal(expected, actual.Count());
        }

        [Fact]
        public async Task FilmManagementService_GetFilmDetailsAsync_VerifyExecution()
        {
            var userId = "abcd";
            var filmId = 2;

            MockFilmRepository_GetFilmAsync();
            MockFilmRatingRepository();
            MockFilmCountriesRequest();
            MockDistributorRepository();
            MockPeopleRepository();

            var filmManagementService = _autoMockContext.Create<FilmManagementService>();
            var actual = await filmManagementService.GetFilmDetailsAsync(filmId, userId);

            Assert.Equal(234234m, actual.BoxOffice);
            Assert.Equal(2342m, actual.Budget);
            Assert.Equal(1, actual.Stars.Count());
            Assert.Equal(1, actual.Directors.Count());
            Assert.Equal(0, actual.Writers.Count());
            Assert.Equal(0, actual.Composers.Count());
        }

        [Fact]
        public async Task FilmManagementService_GetFilmDetailsAsync_VerifyExecutionAndNull()
        {
            var userId = "abcd";
            var filmId = 2;

            MockFilmRepository_GetFilmAsync_Null();

            var filmManagementService = _autoMockContext.Create<FilmManagementService>();
            var actual = await filmManagementService.GetFilmDetailsAsync(filmId, userId);

            Assert.Null(actual);
        }

        [Fact]
        public async Task FilmManagementService_RateMovieAsync_VerifyUpdateRatingAsync()
        {
            var userId = "abcd";

            var rateRequest = new RateRequest
            {
                FilmId = 2,
                Rating = 5
            };

            MockFilmRating_GetPersonalRatingAsync_NotNull();

            var filmManagementService = _autoMockContext.Create<FilmManagementService>();
            await filmManagementService.RateMovieAsync(rateRequest, userId);

            _autoMockContext.Mock<IFilmRatingRepository>()
                .Verify(m => m.UpdateRatingAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<double>()));
        }

        [Fact]
        public async Task FilmManagementService_RateMovieAsync_VerifyAddRatingAsync()
        {
            var userId = "abcd";

            var rateRequest = new RateRequest
            {
                FilmId = 2,
                Rating = 5
            };

            MockFilmRating_GetPersonalRatingAsync_Null();

            var filmManagementService = _autoMockContext.Create<FilmManagementService>();
            await filmManagementService.RateMovieAsync(rateRequest, userId);

            _autoMockContext.Mock<IFilmRatingRepository>()
                .Verify(m => m.AddRatingAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<double>()));
        }

        [Fact]
        public async Task FilmManagementService_LeaveFeedbackAsync_VerifyExecution()
        {
            var userId = "asdfasdf";
            var feedback = new FeedbackRequest
            {
                FilmId = 2,
                Message = "Foo"
            };

            var filmManagementService = _autoMockContext.Create<FilmManagementService>();
            await filmManagementService.LeaveFeedbackAsync(feedback, userId);

            _autoMockContext.Mock<IFeedbackRepository>()
                .Verify(m => m.SaveFeedbackAsync(It.IsAny<Feedback>()));
        }

        private void MockFilmRating_GetPersonalRatingAsync_NotNull()
        {
            _autoMockContext.Mock<IFilmRatingRepository>()
                .Setup(m => m.GetPersonalRatingAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(3.6);
        }

        private void MockFilmRating_GetPersonalRatingAsync_Null()
        {
            _autoMockContext.Mock<IFilmRatingRepository>()
                .Setup(m => m.GetPersonalRatingAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(null);
        }

        private void MockFilmRepository_GetFilmsAsync()
        {
            _autoMockContext.Mock<IFilmRepository>()
                .Setup(m => m.GetFilmsAsync())
                .ReturnsAsync(MockFilmsRequest());
        }

        private void MockFilmRepository_GetFilmAsync()
        {
            _autoMockContext.Mock<IFilmRepository>()
                .Setup(m => m.GetFilmAsync(It.IsAny<long>()))
                .ReturnsAsync(new Film
                {
                    BoxOffice = 234234m,
                    Budget = 2342m,
                    DistributorId = 4,
                    Id = 2,
                    PicturePath = "",
                    SmallPicturePath = "",
                    Plot = "asdf",
                    Title = "ABCD",
                    YearOfRelease = DateTime.Now
                });
        }

        private void MockFilmRepository_GetFilmAsync_Null()
        {
            _autoMockContext.Mock<IFilmRepository>()
                .Setup(m => m.GetFilmAsync(It.IsAny<long>()))
                .ReturnsAsync(null);
        }

        private void MockFilmRatingRepository()
        {
            _autoMockContext.Mock<IFilmRatingRepository>()
               .Setup(m => m.GetTotalRating(It.IsAny<long>()))
               .Returns(4.0);

            MockFilmRating_GetPersonalRatingAsync_NotNull();
        }

        private void MockFeedbackRepository_GetFeedbacksAsync()
        {
            _autoMockContext.Mock<IFeedbackRepository>()
               .Setup(m => m.GetFeedbacksAsync(It.IsAny<long>()))
               .ReturnsAsync(MockFeedbacksRequest());
        }

        private void MockFilmCountryRepository_GetFilmCountriesAsync()
        {
            _autoMockContext.Mock<IFilmCountryRepository>()
               .Setup(m => m.GetFilmCountriesAsync(It.IsAny<long>()))
               .ReturnsAsync(MockFilmCountriesRequest());
        }

        private void MockDistributorRepository()
        {
            _autoMockContext.Mock<IDistributorRepository>()
               .Setup(m => m.GetDistributorAsync(It.IsAny<long>()))
               .ReturnsAsync(new Distributor
               {
                   DateOfFoundation = DateTime.Now,
                   Title = "Warner Brothers"
               });
        }

        private void MockPeopleRepository()
        {
            _autoMockContext.Mock<IFilmPeopleRepository>()
               .Setup(m => m.GetFilmPeopleAsync(It.IsAny<long?>(), It.IsAny<long?>()))
               .ReturnsAsync(MockFilmPeopleRequest());
        }

        private Film[] MockFilmsRequest()
        {
            return new List<Film>
            {
                new Film
                {
                    BoxOffice = 234234m,
                    Budget = 2342m,
                    DistributorId = 4,
                    Id = 2,
                    PicturePath = "",
                    SmallPicturePath = "",
                    Plot = "asdf",
                    Title = "ABCD",
                    YearOfRelease = DateTime.Now
                }
            }
            .ToArray();
        }

        private Feedback[] MockFeedbacksRequest()
        {
            return new List<Feedback>
            {
                new Feedback
                {
                    Text = "asdf",
                    DateOfPublication = DateTime.Now,
                    User = new ApplicationUser
                    {
                        UserName = "Switch"
                    }
                }
            }
            .ToArray();
        }

        private FilmCountry[] MockFilmCountriesRequest()
        {
            return new List<FilmCountry>
            {
                new FilmCountry
                {
                    Country = new Country
                    {
                        Id = 1,
                        Name = "United States"
                    }
                },
                                new FilmCountry
                {
                    Country = new Country
                    {
                        Id = 140,
                        Name = "United Kingdom"
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
                    People = new People
                    {
                        Id = 2,
                        ShortName = "Foo Foo"
                    },
                    Career = new Career
                    {
                        Id = 2
                    }
                },
                new FilmPeople
                {
                    People = new People
                    {
                        Id = 2,
                        ShortName = "Foo Foo"
                    },
                    Career = new Career
                    {
                        Id = 5
                    }
                },
                new FilmPeople
                {
                    People = new People
                    {
                        Id = 2,
                        ShortName = "Foo Foo"
                    },
                    Career = new Career
                    {
                        Id = 6
                    }
                 }
            }
            .ToArray();
        }
    }
}
