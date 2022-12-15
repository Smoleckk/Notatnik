using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Xml.Linq;


namespace Notatnik.Server
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        //[RegularExpression(@"^[a-zA-Z]+$", "Please only enter letters")]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<Note> Notes { get; set; }
    }
}
