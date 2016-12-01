using MovieProject.Models;

namespace Entities.Entities
{
    public class FilmRating
    {
        public long Id { get; set; }
        public double Rating { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public long FilmId { get; set; }
        public virtual Film Film { get; set; }
    }
}
