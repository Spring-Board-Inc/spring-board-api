using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record PhoneDto
    {
        [Required]
        public string PhoneNumber { get; init; }
        public string Ext { get; init; }
    }
}
