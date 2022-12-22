using Notatnik.Server.Valid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik.Shared
{
    public class UserRegisterRequest
    {
        [Required, EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.@]*$", ErrorMessage = "Allowed only leters, numbers and _ .")]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(3), MaxLength(22)]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Allowed only leters, numbers and _ .")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Allowed only leters, numbers and")]
        //[ValidPass]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Allowed only leters, numbers and")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
