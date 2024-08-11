using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Authors
{
    public class UpdateAuthorRequest: InsertAuthorRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
