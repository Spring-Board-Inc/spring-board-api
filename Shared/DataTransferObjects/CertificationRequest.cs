﻿using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class CertificationRequest
    {
        [Required(ErrorMessage = $"{nameof(Name)} is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = $"{nameof(IssuingBody)} is required")]
        public string IssuingBody { get; set; }
        public DateTime? IssuingDate { get; set; } = null;
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(IssuingBody);
    }
}
