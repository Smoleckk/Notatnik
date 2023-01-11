using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik.Shared.Dtos.NoteDto
{
    public class NoteDisplayDto
    {
        public int NoteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool Secure { get; set; } = false;
        public bool Public { get; set; } = true;
    }
}
