using Moq;
using Xunit;
using Autofac.Extras.Moq;
using MovieProject.Services;
using System.Threading.Tasks;
using Entities.Entities;
using Entities.Models.Requests;
using System;
using Repositories.Repositories.Interfaces;
using System.Linq;

namespace Tests
{
    public class ScreeningManagementServiceTests
    {
        private AutoMock _autoMockContext { get; }

        public ScreeningManagementServiceTests()
        {
            _autoMockContext = AutoMock.GetLoose();
        }

        [Fact]
        public async Task ScreeningManagementService_GetTheatresAsync_VerifyExecution()
        {
            var request = GetTheatreRequest(45);

            MockFilmTheatreRepository_GetFilmTheatresAsync();

            var expectedLength = GetFilmTheatres().Length;
            var expectedBoxOfficeInPercentage = 50;

            var screeningManagementService = _autoMockContext.Create<ScreeningManagementService>();
            var actual = await screeningManagementService.GetTheatresAsync(request);

            Assert.Equal(expectedLength, actual.Count());
            Assert.Equal(expectedBoxOfficeInPercentage, actual.First().PercentageOfBoxOffice);
        }

        [Fact]
        public async Task ScreeningManagementService_GetTheatresAsync_VerifyNull()
        {
            var request = GetTheatreRequest(null);

            MockFilmTheatreRepository_GetFilmTheatresAsync();

            var screeningManagementService = _autoMockContext.Create<ScreeningManagementService>();
            var actual = await screeningManagementService.GetTheatresAsync(request);

            Assert.Null(actual);
        }

        [Fact]
        public async Task ScreeningManagementService_SubmitTheatreInformationAsync_NotAllPropertiesInitialized_Error()
        {
            var status = "Error";
            var message = "Submitting failed! Values cannot be null, please fill empty fields.";

            var request = GetUpdateFilmTheatreRequest();

            var screeningManagementService = _autoMockContext.Create<ScreeningManagementService>();
            var actual = await screeningManagementService.SubmitTheatreInformationAsync(request);

            Assert.Equal(status, actual.Status);
            Assert.Equal(message, actual.Message);
        }

        [Fact]
        public async Task ScreeningManagementService_SubmitTheatreInformationAsync_SuccessfullyAdding_Success()
        {
            var status = "Success";
            var message = "Successfully submitted! Entity has been successfully added.";

            MockFilmTheatreRepository_Null();
            MockFilmRepository();
            MockFilmTheatreRepository_GetFilmTheatresExceptAsync();

            var request = GetUpdateFilmTheatreRequest(2, 2, DateTime.Now, DateTime.Now, 3234, 234034.00m);

            var screeningManagementService = _autoMockContext.Create<ScreeningManagementService>();
            var actual = await screeningManagementService.SubmitTheatreInformationAsync(request);

            Assert.Equal(status, actual.Status);
            Assert.Equal(message, actual.Message);
        }

        [Fact]
        public async Task ScreeningManagementService_SubmitTheatreInformationAsync_MovieAlreadyOnScreen_Error()
        {
            var status = "Error";
            var message = "Submitting failed! Theare is a screening of another movie during this time.";

            MockFilmTheatreRepository();
            MockFilmTheatreRepository_GetFilmTheatresAsync_LongOverload();
            MockFilmRepository();
            MockFilmTheatreRepository_GetFilmTheatresExceptAsync();

            var request = GetUpdateFilmTheatreRequest(2, 2, DateTime.Now, DateTime.Now.AddDays(23), 3234, 234034.00m);

            var screeningManagementService = _autoMockContext.Create<ScreeningManagementService>();
            var actual = await screeningManagementService.SubmitTheatreInformationAsync(request);

            Assert.Equal(status, actual.Status);
            Assert.Equal(message, actual.Message);
        }

        [Fact]
        public async Task ScreeningManagementService_SubmitTheatreInformationAsync_SuccessfullyUpdating_Success()
        {
            var status = "Success";
            var message = "Successfully submitted! Entity has been successfully updated.";

            MockFilmTheatreRepository();
            MockFilmTheatreRepository_GetFilmTheatresAsync_LongOverload();
            MockFilmRepository();
            MockFilmTheatreRepository_GetFilmTheatresExceptAsync();

            var request = GetUpdateFilmTheatreRequest(2, 2, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(-2), 3234, 234034.00m);

            var screeningManagementService = _autoMockContext.Create<ScreeningManagementService>();
            var actual = await screeningManagementService.SubmitTheatreInformationAsync(request);

            Assert.Equal(status, actual.Status);
            Assert.Equal(message, actual.Message);
        }

        [Fact]
        public async Task ScreeningManagementService_SubmitTheatreInformationAsync_VerifyUpdateExecution()
        {
            MockFilmTheatreRepository();
            MockFilmTheatreRepository_GetFilmTheatresAsync_LongOverload();
            MockFilmRepository();
            MockFilmTheatreRepository_GetFilmTheatresExceptAsync();

            var request = GetUpdateFilmTheatreRequest(2, 2, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(-2), 3234, 234034.00m);

            var screeningManagementService = _autoMockContext.Create<ScreeningManagementService>();
            var actual = await screeningManagementService.SubmitTheatreInformationAsync(request);

            _autoMockContext.Mock<IFilmTheatreRepository>()
                .Verify(m => m.UpdateFilmTheatreAsync(It.IsAny<FilmTheatre>()), Times.Once);
        }

        [Fact]
        public async Task ScreeningManagementService_SubmitTheatreInformationAsync_VerifyAddExecution()
        {
            MockFilmTheatreRepository_Null();
            MockFilmRepository();
            MockFilmTheatreRepository_GetFilmTheatresExceptAsync();

            var request = GetUpdateFilmTheatreRequest(2, 2, DateTime.Now, DateTime.Now, 3234, 234034.00m);

            var screeningManagementService = _autoMockContext.Create<ScreeningManagementService>();
            var actual = await screeningManagementService.SubmitTheatreInformationAsync(request);

            _autoMockContext.Mock<IFilmTheatreRepository>()
                .Verify(m => m.AddFilmTheatreAsync(It.IsAny<FilmTheatre>()), Times.Once);
        }

        private void MockFilmTheatreRepository_GetFilmTheatresAsync_LongOverload()
        {
            _autoMockContext.Mock<IFilmTheatreRepository>()
                .Setup(m => m.GetFilmTheatresAsync(It.IsAny<long>()))
                .ReturnsAsync(GetFilmTheatres());
        }

        private void MockFilmTheatreRepository_GetFilmTheatresAsync()
        {
            _autoMockContext.Mock<IFilmTheatreRepository>()
                .Setup(m => m.GetFilmTheatresAsync(It.IsAny<TheatreRequest>()))
                .ReturnsAsync(GetFilmTheatres());
        }

        private void MockFilmTheatreRepository()
        {
            _autoMockContext.Mock<IFilmTheatreRepository>()
                .Setup(m => m.GetFilmTheatreAsync(It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(new FilmTheatre
                {
                    FilmId = 2,
                    TheatreId = 2
                });
        }

        private void MockFilmTheatreRepository_Null()
        {
            _autoMockContext.Mock<IFilmTheatreRepository>()
                .Setup(m => m.GetFilmTheatreAsync(It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(null);
        }

        private void MockFilmRepository()
        {
            _autoMockContext.Mock<IFilmRepository>()
                .Setup(m => m.GetFilmAsync(It.IsAny<long>()))
                .ReturnsAsync(new Film
                {
                    BoxOffice = 234234234
                });
        }

        private void MockFilmTheatreRepository_GetFilmTheatresExceptAsync()
        {
            _autoMockContext.Mock<IFilmTheatreRepository>()
                .Setup(m => m.GetFilmTheatresExceptAsync(It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(GetFilmTheatres());
        }

        private UpdateFilmTheatreRequest GetUpdateFilmTheatreRequest(
            long? theatreId = null,
            long? filmId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? amount = null,
            decimal? boxOffice = null)
        {
            return new UpdateFilmTheatreRequest
            {
                FilmId = filmId,
                TheatreId = theatreId,
                AmountOfPeople = amount,
                BoxOffice = boxOffice,
                StartDistributionDate = startDate,
                EndDistributionDate = endDate
            };
        }

        private TheatreRequest GetTheatreRequest(long? filmId)
        {
            return new TheatreRequest
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + new TimeSpan(10, 0, 0),
                FilmId = filmId
            };
        }

        private FilmTheatre[] GetFilmTheatres()
        {
            return new FilmTheatre[]
            {
                new FilmTheatre
                {
                    FilmId = 2,
                    TheatreId = 2, 
                    StartDistributionDate = DateTime.Now,
                    EndDistributionDate = DateTime.Now.AddMonths(1),
                    BoxOfficePerMovie = 1000,
                    Theatre = new Theatre
                    {
                        Title = "Foo",
                        Country = new Country
                        {
                            Name = "Foo"
                        }
                    }
                },
                new FilmTheatre
                {
                    FilmId = 4,
                    TheatreId = 2,
                    StartDistributionDate = DateTime.Now,
                    EndDistributionDate = DateTime.Now.AddMonths(1),
                    BoxOfficePerMovie = 1000,
                    Theatre = new Theatre
                    {
                        Title = "Foo",
                        Country = new Country
                        {
                            Name = "Foo"
                        }
                    }
                }
            };
        }
    }
}
