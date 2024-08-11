namespace ReadRealmBackend.Models.Requests.Genres
{
    public class UpdateGenreRequest: InsertGenreRequest
    {
        public int Id { get; set; }
    }
}