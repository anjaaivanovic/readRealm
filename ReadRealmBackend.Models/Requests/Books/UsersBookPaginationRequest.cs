using System.ComponentModel.DataAnnotations;
using ReadRealmBackend.Models.Requests.Generic;

namespace ReadRealmBackend.Models.Requests.Books
{
    public class UsersBookPaginationRequest: GenericPaginationRequest
    {
        [Required]
        public int StatusId { get; set; }
    }
}