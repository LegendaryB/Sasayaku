using Sasayaku.Data;

using System.ComponentModel.DataAnnotations;

namespace Sasayaku.Configuration
{
    public class AuthenticationOptions
    {
        public const string Section = "Authentication";

        [Required]
        public required ClientCredentials Superuser { get; set; }

        [Required]
        public required string JwtSecretKey { get; set; }
    }
}
