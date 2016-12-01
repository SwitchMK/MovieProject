using System;

namespace Entities.Models.Requests
{
    public class FilmsInTheatreRequest
    {
        public long? TheatreId { get; set; }
        public DateTime? Day { get; set; }
    }
}
