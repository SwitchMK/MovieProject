using System;

namespace Entities.Models.Responses
{
    public class SmallMovieResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime YearOfRelease { get; set; }
    }
}
