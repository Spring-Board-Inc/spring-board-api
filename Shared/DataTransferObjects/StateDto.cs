using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record StateDto
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string AdminArea { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public record CountryDto(
        Guid Id, DateTime CreatedAt,
        DateTime UpdatedAt, string Name
        );

    public record StateForCreationDto
    {
        [Required]
        public Guid CountryId { get; set; }
        [Required]
        public string AdminArea { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(AdminArea) && CountryId != Guid.Empty;
    }

    public record StateForUpdateDto
    {
        [Required]
        public Guid CountryId { get; set; }
        [Required]
        public string AdminArea { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(AdminArea) && CountryId != Guid.Empty;
    }

    public record CountryForCreationDto
    {
        [Required]
        public string Name { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Name);
    }

    public record CountryForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Name);
    }
}
