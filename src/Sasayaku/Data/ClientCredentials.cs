using System.ComponentModel.DataAnnotations;

namespace Sasayaku.Data
{
    public class ClientCredentials
    {
        [Required]
        public string ClientId { get; set; } = default!;

        [Required]
        public string ClientSecret { get; set; } = default!;
    }
}
