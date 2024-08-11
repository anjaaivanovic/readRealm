using System.ComponentModel.DataAnnotations;

namespace ReadRealmBackend.Models.Requests.Statuses
{
    public class InsertStatusRequest
    {
        [Required]
        public string Name { get; set; }
    }
}