using System.ComponentModel.DataAnnotations;

namespace Sasayaku.Configuration
{
    public class AppOptions
    {
        public const string Section = "Sasayaku";

        [Required]
        public required AuthenticationOptions Authentication { get; set; }
    }
}
