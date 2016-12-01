namespace Entities.Entities
{
    public class FilmCountry
    {
        public long Id { get; set; }
        public long FilmId { get; set; }
        public virtual Film Film { get; set; }
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
