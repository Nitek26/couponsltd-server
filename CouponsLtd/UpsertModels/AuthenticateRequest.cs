using System.ComponentModel.DataAnnotations;

namespace CouponsLtd.UpsertModels
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}