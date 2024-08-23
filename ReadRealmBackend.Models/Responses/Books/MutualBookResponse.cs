namespace ReadRealmBackend.Models.Responses.Books
{
    public class MutualBookResponse: RecommendedBookResponse
    {
        public List<string> UserIds { get; set; }
    }
}