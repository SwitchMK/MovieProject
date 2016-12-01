using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class People
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string SmallPicturePath { get; set; }
        public string PicturePath { get; set; }
        public string Biography { get; set; }

        public virtual ICollection<PeopleCareer> Careers { get; set; }
        public virtual ICollection<FilmPeople> Films { get; set; }
    }
}
