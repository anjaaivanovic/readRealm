using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.BookAuthors
{
    public class InsertBookUserRequest
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CurrentChapter { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [Range(0, 5)]
        public decimal? Rating { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}