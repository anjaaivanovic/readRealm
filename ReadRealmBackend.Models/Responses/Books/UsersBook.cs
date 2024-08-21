using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.Models.Responses.Books
{
    public class UsersBook
    {
        public Book Book { get; set; }
        public BookUser BookUser { get; set; }
    }
}