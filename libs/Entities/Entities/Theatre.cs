namespace Entities.Entities
{
    public class Theatre
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string Title { get; set; }
    }
}
