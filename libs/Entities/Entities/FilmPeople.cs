namespace Entities.Entities
{
    public class FilmPeople
    {
        public long Id { get; set; }
        public long FilmId { get; set; }
        public Film Film { get; set; }
        public long PeopleId { get; set; }
        public People People { get; set; }
        public long CareerId { get; set; }
        public Career Career { get; set; }
    }
}
