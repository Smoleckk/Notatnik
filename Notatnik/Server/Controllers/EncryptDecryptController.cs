using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Notatnik.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptDecryptController : ControllerBase
    {
        [HttpGet("Encrypt")]
        public string Encrypt(string text)
        {
            var encryptString = EncryptDecryptManager.Encrypt(text);
            return encryptString;
        }
        [HttpGet("Decrypt")]

        public string Decrypt(string text)
        {
            var decryptString = EncryptDecryptManager.Decrypt(text);
            return decryptString;
        }
    }
}
