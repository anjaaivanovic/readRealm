using System.ComponentModel.DataAnnotations;
using ReadRealmBackend.Models.Requests.Generic;

namespace ReadRealmBackend.Models.Requests.Notes
{
    public class BookNotePaginationRequest: GenericPaginationRequest
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int Visibility { get; set; }
    }
}
