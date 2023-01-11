using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik.Shared.Dtos.UserDto
{
    public class UserLoginCaptcha
    {
        [Required, EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.@]*$", ErrorMessage = "Allowed only leters, numbers and _ . @")]

        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Allowed only leters, numbers and _ . ")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(CaptchaText1), ErrorMessage = "The entered `Security Number` is not correct.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Allowed only numbers")]
        public string EnteredCaptchaText1 { set; get; }

        public string CaptchaText1 { set; get; }
    }
}
