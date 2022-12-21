using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik.Shared
{
    public class UserLoginCaptcha
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(CaptchaText1), ErrorMessage = "The entered `Security Number` is not correct.")]
        public string EnteredCaptchaText1 { set; get; }

        public string CaptchaText1 { set; get; }
    }
}
