using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieProject.Models;
using Entities.Entities;

namespace Entities.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<FilmRating> FilmRatings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<FilmTheatre> FilmTheatres { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
