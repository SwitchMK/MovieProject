using Autofac;
using Entities.Contexts;
using Microsoft.EntityFrameworkCore;
using MovieProject.Services;
using MovieProject.Services.Interfaces;
using Repositories.Repositories;
using Repositories.Repositories.Interfaces;

namespace MovieProject.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Contexts
            builder.RegisterType<ApplicationDbContext>()
                .As<DbContext>();

            // Repositories
            builder.RegisterType<FilmRepository>()
                .As<IFilmRepository>();

            builder.RegisterType<PeopleRepository>()
                .As<IPeopleRepository>();

            builder.RegisterType<DistributorRepository>()
                .As<IDistributorRepository>();

            builder.RegisterType<FeedbackRepository>()
                .As<IFeedbackRepository>();

            builder.RegisterType<FilmCountryRepository>()
                .As<IFilmCountryRepository>();

            builder.RegisterType<FilmPeopleRepository>()
                .As<IFilmPeopleRepository>();

            builder.RegisterType<PeopleCareerRepository>()
                .As<IPeopleCareerRepository>();

            builder.RegisterType<FilmRatingRepository>()
                .As<IFilmRatingRepository>();

            builder.RegisterType<FilmTheatreRepository>()
                .As<IFilmTheatreRepository>();

            builder.RegisterType<TheatreRepository>()
                .As<ITheatreRepository>();

            // Services
            builder.RegisterType<FilmManagementService>()
                .As<IFilmManagementService>();

            builder.RegisterType<PeopleManagementService>()
                .As<IPeopleManagementService>();

            builder.RegisterType<ScreeningManagementService>()
                .As<IScreeningManagementService>();
        }
    }
}
