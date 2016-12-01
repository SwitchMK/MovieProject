using System.Collections.Generic;

namespace Entities.Entities
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FilmCountry> Films { get; set; }
    }
}
