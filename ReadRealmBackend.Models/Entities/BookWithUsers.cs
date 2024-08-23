namespace ReadRealmBackend.Models.Entities
{
    public class BookWithUsers
    {
        public Book Book { get; set; }
        public List<string> BookUsers { get; set; }
    }
}