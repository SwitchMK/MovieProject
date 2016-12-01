using System.Collections.Generic;

namespace Entities.Entities
{
    public class Career
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<PeopleCareer> People { get; set; }
    }
}
