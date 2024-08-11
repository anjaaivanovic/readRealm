using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Languages
{
    public class InsertLanguageRequest
    {
        [Required]
        public string Name { get; set; }
    }
}