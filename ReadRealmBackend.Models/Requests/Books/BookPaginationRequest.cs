using ReadRealmBackend.Models.Requests.Generic;

namespace ReadRealmBackend.Models.Requests.Books
{
    public class BookPaginationRequest: GenericPaginationRequest
    {
        public string? Search {  get; set; }
        public int? GenreId { get; set; }
        public int? AuthorId { get; set; }
        public int? LanguageId { get; set; }
        public int? BookTypeId { get; set; }
        public int? MaxWordCount { get; set; }
        public int? MinWordCount { get; set; }
        public bool? Mutual { get; set; }
    }
}