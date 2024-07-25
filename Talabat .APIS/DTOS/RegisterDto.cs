using System.ComponentModel.DataAnnotations;

namespace Talabat_.APIS.DTOS
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10$})(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%&amp;*()_+]).*$",
            ErrorMessage = "Password must be contains 1 uppercase, 1 lowercase, 1 digit, 1 speacial character !")]
        public string Password { get; set; }
    }
}
