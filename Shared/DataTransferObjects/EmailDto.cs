using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record EmailDto
    {
        [Required, EmailAddress]
        public string EmailAddress { get; init; }
    }
}
