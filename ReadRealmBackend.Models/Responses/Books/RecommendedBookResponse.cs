namespace ReadRealmBackend.Models.Responses.Books
{
    public class RecommendedBookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BriefDescription { get; set; }
        public string ISBN { get; set; }
        public List<string> Genres { get; set; }
    }
}