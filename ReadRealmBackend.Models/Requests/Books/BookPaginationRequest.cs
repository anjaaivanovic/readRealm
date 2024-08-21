using ReadRealmBackend.Models.Requests.Generic;

namespace ReadRealmBackend.Models.Requests.Books
{
    public class BookPaginationRequest: GenericPaginationRequest
    {
        public int? GenreId { get; set; }
        public int? AuthorId { get; set; }
        public int? LanguageId { get; set; }
        public int? BookTypeId { get; set; }
        public int? MaxWordCount { get; set; }
        public int? MinWordCount { get; set; }
    }
}