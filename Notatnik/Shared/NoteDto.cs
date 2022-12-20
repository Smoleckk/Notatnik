using Notatnik.Server.Valid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik.Shared
{
    public class NoteDto
    {
        [Required, MinLength(6)]
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool Secure { get; set; } = true;
        public bool Public { get; set; } = false;
        public string Password { get; set; } = string.Empty;

    }
}
