using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
