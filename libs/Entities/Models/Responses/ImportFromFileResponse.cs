using System;

namespace Entities.Models.Responses
{
    public class ImportFromFileResponse
    {
        public long? TheatreId { get; set; }
        public long? FilmId { get; set; }
        public decimal? BoxOffice { get; set; }
        public int? AmountOfPeople { get; set; }
        public DateTime? StartDistributionDate { get; set; }
        public DateTime? EndDistributionDate { get; set; }
    }
}
