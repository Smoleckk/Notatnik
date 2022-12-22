using Notatnik.Server.Valid;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Notatnik.Server
{
    public class Note
    {
        public int NoteId { get; set; }
        [Required, MinLength(6)]
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool Secure { get; set; } = false;
        public bool Public { get; set; } = false;

        public byte[] NoteHash { get; set; } = new byte[32];
        public byte[] NoteSalt { get; set; } = new byte[32];

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
