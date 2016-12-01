using System;

namespace Entities.Models.Requests
{
    public class TheatreRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? FilmId { get; set; }
    }
}
