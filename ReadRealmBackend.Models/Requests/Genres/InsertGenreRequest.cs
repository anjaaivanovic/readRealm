using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Genres
{
    public class InsertGenreRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
