using Notatnik.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Notatnik.Server.Valid
{
    public class ValidPass : ValidationAttribute
    {
        protected override ValidationResult
              IsValid(object value, ValidationContext validationContext)
        {
            string pass = value.ToString();
            int passLength = pass.Length;
            //z passLength znaków(małych liter[a - z], duzych liter[A - Z], cyfr[0 - 9] oraz) 26+26++10
            double entropyPerChar = Math.Log2(62);
            int entropy = (int)(passLength * entropyPerChar);

            if (entropy < 30)
            {
                return new ValidationResult
                    ("Twoje hasło jest bardzo słabe " + entropy);
            }
            if (entropy < 40)
            {
                return new ValidationResult
                    ("Twoje hasło nie jest jeszcze wystarczające " + entropy);
            }
            if (entropy < 60)
            {
                return new ValidationResult
                    ("Możesz się jeszcze postarać! " + entropy);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
