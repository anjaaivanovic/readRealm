using ReadRealmBackend.Models.Responses.Notes;

namespace ReadRealmBackend.Models.Responses.Books
{
    public class BookResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateOnly Published { get; set; }

        public string Description { get; set; }

        public int ChapterCount { get; set; }

        public int WordCount { get; set; }

        public string Isbn { get; set; }

        public int? TypeId { get; set; }

        public List<NoteResponse> Notes { get; set; }

        public string TypeName { get; set; }

        public List<string> Authors { get; set; }

        public List<string> Genres { get; set; }

        public List<string> Languages { get; set; }
        public decimal Rating { get; set; }
        public List<NoteResponse> FinalThoughts { get; set; }
    }
}
