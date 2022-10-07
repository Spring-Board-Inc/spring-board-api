using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UserNamesForUpdateDto
    {
        [Required(ErrorMessage = $"{nameof(FirstName)} is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = $"{nameof(LastName)} is required")]
        public string LastName { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
    }
}
