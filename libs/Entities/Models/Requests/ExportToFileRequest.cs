namespace Entities.Models.Requests
{
    public class ExportToFileRequest
    {
        public string Path { get; set; }
        public UpdateFilmTheatreRequest UpdateFilmTheatreRequest { get; set; }
    }
}
