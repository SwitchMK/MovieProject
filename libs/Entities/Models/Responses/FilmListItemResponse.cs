using System;

namespace Entities.Models.Responses
{
    public class FilmListItemResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime YearOfRelease { get; set; }
        public double? TotalRating { get; set; }
        public double? PersonalRating { get; set; }
        public string SmallPicturePath { get; set; }
    }
}
