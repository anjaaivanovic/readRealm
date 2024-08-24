using System.ComponentModel.DataAnnotations.Schema;

namespace ReadRealmBackend.Models.Entities
{
    public partial class Book
    {
        [NotMapped]
        public List<Note> FinalThoughts { get; set; }

        [NotMapped]
        public decimal Rating { get; set; }
    }
}
