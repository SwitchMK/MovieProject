using System;

namespace Entities.Entities
{
    public class FilmTheatre
    {
        public long Id { get; set; }
        public long FilmId { get; set; }
        public virtual Film Film { get; set; }
        public long TheatreId { get; set; }
        public virtual Theatre Theatre { get; set; }
        public DateTime StartDistributionDate { get; set; }
        public DateTime EndDistributionDate { get; set; }
        public int AmountOfPeople { get; set; }
        public decimal BoxOfficePerMovie { get; set; }
    }
}
