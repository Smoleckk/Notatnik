using Notatnik.Server.Valid;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Notatnik.Shared.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        [Required, MinLength(3), MaxLength(22)]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Allowed only leters, numbers and _ .")]
        public string Title { get; set; } = string.Empty;
        [MaxLength(1000)]
        //[RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Allowed only leters, numbers and _ .")]
        public string Body { get; set; } = string.Empty;
        public bool Secure { get; set; } = false;
        public bool Public { get; set; } = false;

        public byte[] NoteHash { get; set; } = new byte[32];
        public byte[] NoteSalt { get; set; } = new byte[32];

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
