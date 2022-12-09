using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik.Shared
{
    public class NoteDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Secure { get; set; } = true;
        public bool Public { get; set; } = false;
        public string Password { get; set; } = string.Empty;

    }
}
