using System;
using System.Collections.Generic;

namespace Entities.Models.Responses
{
    public class FullPeopleDetailsResponse
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Biography { get; set; }
        public string SmallPicturePath { get; set; }
        public string PicturePath { get; set; }
        public IEnumerable<string> Careers { get; set; }
        public IEnumerable<SmallMovieResponse> Films { get; set; }
    }
}
