using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.BookTypes
{
    public class UpdateBookTypeRequest: InsertBookTypeRequest
    {
        [Required]
        public int Id {  get; set; }
    }
}