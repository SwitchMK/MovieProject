using System;

namespace Entities.Models.Responses
{
    public class PeopleListItemResponse
    {
        public long Id { get; set; }
        public string ShortName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SmallPicturePath { get; set; }
    }
}