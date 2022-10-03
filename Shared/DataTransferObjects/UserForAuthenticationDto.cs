﻿using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; init; }
        public bool RememberMe { get; init; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(UserName);
    }
}
