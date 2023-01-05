﻿using Notatnik.Server.Valid;
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
        [Required, MinLength(3), MaxLength(22)]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$", ErrorMessage = "Allowed only leters, numbers and _ .")]
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool Secure { get; set; } = true;
        public bool Public { get; set; } = false;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Allowed only leters, numbers and")]
        public string Password { get; set; } = string.Empty;

    }
}
