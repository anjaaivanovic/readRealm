using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Generic
{
    public class GenericPaginationRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Page { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ItemCount { get; set; }

        [Required]
        public string Sort { get; set; }

        [Required]
        public bool IsAsc { get; set; }

        public string? Search { get; set; }
    }
}