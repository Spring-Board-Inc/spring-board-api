using Entities.Models;

namespace Shared.DataTransferObjects
{
    public class DetailedUserToReturnDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
    }
}
