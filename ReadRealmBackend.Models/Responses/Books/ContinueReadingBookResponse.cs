namespace ReadRealmBackend.Models.Responses.Books
{
    public class ContinueReadingBookResponse: RecommendedBookResponse
    {
        public List<string> Authors { get; set; }
        public DateOnly StartedOn { get; set; }
        public int TotalChapters { get; set; }
        public float Rating { get; set; }
        public int CurrentChapter { get; set; }
    }
}