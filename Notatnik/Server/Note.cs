using System.Reflection.Metadata;

namespace Notatnik.Server
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Secure { get; set; } = true;
        public bool Public { get; set; } = false;

        public byte[] NoteHash { get; set; }
        public byte[] NoteSalt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
