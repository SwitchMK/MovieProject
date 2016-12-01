namespace Entities.Entities
{
    public class PeopleCareer
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public virtual People Person { get; set; }
        public long CareerId { get; set; }
        public virtual Career Career { get; set; }
    }
}
