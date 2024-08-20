using ReadRealmBackend.Models.Requests.BookAuthors;

namespace ReadRealmBackend.Models.Requests.BookUsers
{
    public class InsertBookUserFullRequest: InsertBookUserRequest
    {
        public string UserId { get; set; }
    }
}