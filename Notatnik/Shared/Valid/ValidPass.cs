using Notatnik.Shared;
using System.ComponentModel.DataAnnotations;

namespace Notatnik.Server.Valid
{
    public class ValidPass : ValidationAttribute
    {
        protected override ValidationResult
              IsValid(object value, ValidationContext validationContext)
        {
            string pass = value.ToString();

            if (pass.Count() < 6)
            {
                return new ValidationResult
                    ("Pass entrophy too low" + Entropy(pass));
            }
            //else if (_lastDeliveryDate > DateTime.Now)
            //{
            //    return new ValidationResult
            //        ("Last Delivery Date can not be greater than current date.");
            //}
            else
            {
                return ValidationResult.Success;
            }
        }
        public static int Entropy(string s)
        {
            var d = new Dictionary<char, bool>();
            foreach (char c in s)
                if (!d.ContainsKey(c)) d.Add(c, true);
            return d.Count();
        }
    }
}
