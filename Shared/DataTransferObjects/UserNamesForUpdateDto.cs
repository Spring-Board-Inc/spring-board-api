using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UserNamesForUpdateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
