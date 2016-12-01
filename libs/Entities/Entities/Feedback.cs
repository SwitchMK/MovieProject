using MovieProject.Models;
using System;

namespace Entities.Entities
{
    public class Feedback
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long FilmId { get; set; }
        public virtual Film Film { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime DateOfPublication { get; set; }
    }
}
