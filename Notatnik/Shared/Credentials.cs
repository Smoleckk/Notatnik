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
        //[MinLength(4)]
        public string Password { get; set; } = string.Empty;
    }
}
