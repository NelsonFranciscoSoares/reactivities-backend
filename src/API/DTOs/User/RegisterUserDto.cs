using System.ComponentModel.DataAnnotations;

namespace API.DTOs.User
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password must be complex")]
        public string Password { get; set; }
        
        [Required]
        public string DisplayName { get; set; }
        
        [Required]
        public string Username { get; set; }
    }
}