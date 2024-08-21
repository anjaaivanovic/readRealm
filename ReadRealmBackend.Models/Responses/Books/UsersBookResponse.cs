namespace ReadRealmBackend.Models.Responses.Books
{
    public class UsersBookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string ISBN { get; set; }
        public int CurrentChapter { get; set; }
        public int ChapterCount { get; set; }
    }
}