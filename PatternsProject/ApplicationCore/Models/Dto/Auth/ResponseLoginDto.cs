﻿using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Dto.Auth
{
    public record ResponseLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required] 
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}
