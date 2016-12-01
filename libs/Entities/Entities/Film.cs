using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class Film
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime YearOfRelease { get; set; }
        public decimal Budget { get; set; }
        public decimal BoxOffice { get; set; }
        public long DistributorId { get; set; }
        public string SmallPicturePath { get; set; }
        public string PicturePath { get; set; }
        public string Plot { get; set; }
        public virtual Distributor Distributor { get; set; }
        
        public virtual ICollection<FilmCountry> Countries { get; set; }
        public virtual ICollection<FilmPeople> People { get; set; }
    }
}
