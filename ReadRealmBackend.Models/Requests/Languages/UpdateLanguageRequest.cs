using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Languages
{
    public class UpdateLanguageRequest: InsertLanguageRequest
    {
        [Required]
        public int Id { get; set; }
    }
}