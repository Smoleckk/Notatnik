using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Xml.Linq;


namespace Notatnik.Shared.Models
{
    public class User
    {
        public int UserId { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.@]*$", ErrorMessage = "Allowed only leters, numbers and _ . @")]
        public string Email { get; set; } = string.Empty;
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Allowed only leters, numbers and _ . ")]
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public int NumberOfLoginTry { get; set; } = 0;
        public DateTime LoginLastTry { get; set; } = DateTime.Now.AddHours(1);
        public DateTime LoginBlockUntil { get; set; } = DateTime.Now;

        public List<Note> Notes { get; set; }
    }
}
