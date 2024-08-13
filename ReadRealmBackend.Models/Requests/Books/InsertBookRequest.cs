using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Books
{
    public class InsertBookRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Published { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ChapterCount { get; set; }

        [Required]
        public int WordCount { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public string BriefDescription { get; set; }

        [Required]
        public List<int> AuthorIds { get; set; }

        [Required]
        public List<int> GenreIds { get; set; }

        [Required]
        public List<int> LanguageIds { get; set; }
    }
}