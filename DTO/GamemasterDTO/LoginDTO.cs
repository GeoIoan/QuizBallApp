using System.ComponentModel.DataAnnotations;

namespace QuizballApp.DTO.GamemasterDTO
{
    public class LoginDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2 - 50 characters")]
        [Required(ErrorMessage = "This field is required")]
        public string? Username { get; set; }


        [StringLength(32, ErrorMessage = "password should not exceed 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$",
         ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character")]
        [Required(ErrorMessage = "This field is required")]
        public string? Password { get; set; }
    }
}
