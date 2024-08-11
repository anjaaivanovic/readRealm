using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Authors
{
    public class InsertAuthorRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
