using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik.Shared
{
    public class Credentials
    {
        //[Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Allowed only leters, numbers and")]
        public string Password { get; set; }
    }
}
