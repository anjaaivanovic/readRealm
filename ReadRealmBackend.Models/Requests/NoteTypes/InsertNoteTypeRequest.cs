using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.NoteTypes
{
    public class InsertNoteTypeRequest
    {
        [Required]
        public string Name { get; set; }
    }
}