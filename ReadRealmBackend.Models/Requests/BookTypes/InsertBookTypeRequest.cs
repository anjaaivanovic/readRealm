using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.BookTypes
{
    public class InsertBookTypeRequest
    {
        [Required]
        public string Name { get; set; }
    }
}