namespace ReadRealmBackend.Models.Requests.Notes
{
    public class InsertNoteFullRequest: InsertNoteRequest
    {
        public string UserId { get; set; }
    }
}