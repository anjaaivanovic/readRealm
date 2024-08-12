namespace ReadRealmBackend.Models.Responses.Books
{
    public class RecommendedBookByFriendsActivityResponse: RecommendedBookResponse
    {
        public string Friend {  get; set; }
        public int WordCount { get; set; }
        public float ReadingTime { get; set; }
        public string? FriendQuote { get; set; }
        public float Rating { get; set; }
    }
}