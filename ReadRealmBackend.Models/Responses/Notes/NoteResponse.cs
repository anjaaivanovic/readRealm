namespace ReadRealmBackend.Models.Responses.Notes
{
    public class NoteResponse
    {
        public string UserId { get; set; }

        public int BookId { get; set; }

        public int Chapter { get; set; }

        public DateOnly DatePosted { get; set; }

        public string Text { get; set; }

        public int Private { get; set; }

        public int? TypeId { get; set; }

    }
}