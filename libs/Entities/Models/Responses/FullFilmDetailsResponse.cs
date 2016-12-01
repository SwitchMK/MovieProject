using System;
using System.Collections.Generic;

namespace Entities.Models.Responses
{
    public class FullFilmDetailsResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime YearOfRelease { get; set; }
        public double? TotalRating { get; set; }
        public double? PersonalRating { get; set; }
        public string PicturePath { get; set; }
        public decimal Budget { get; set; }
        public decimal BoxOffice { get; set; }
        public string Distributor { get; set; }
        public string Plot { get; set; }
        public IEnumerable<string> Countries { get; set; }
        public IEnumerable<SmallPeopleResponse> Directors { get; set; }
        public IEnumerable<SmallPeopleResponse> Producers { get; set; }
        public IEnumerable<SmallPeopleResponse> Stars { get; set; }
        public IEnumerable<SmallPeopleResponse> Writers { get; set; }
        public IEnumerable<SmallPeopleResponse> Composers { get; set; }
    }
}
