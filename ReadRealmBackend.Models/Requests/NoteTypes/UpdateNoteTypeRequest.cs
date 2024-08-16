using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.NoteTypes
{
    public class UpdateNoteTypeRequest: InsertNoteTypeRequest
    {
        [Required]
        public int Id { get; set; }
    }
}