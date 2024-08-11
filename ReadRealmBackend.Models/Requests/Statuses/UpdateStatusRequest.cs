using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Statuses
{
    public class UpdateStatusRequest: InsertStatusRequest
    {
        [Required]
        public int Id { get; set; }
    }
}