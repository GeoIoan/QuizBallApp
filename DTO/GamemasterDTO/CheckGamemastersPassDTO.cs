using System.ComponentModel.DataAnnotations;

namespace QuizballApp.DTO.GamemasterDTO
{

    ///<summary>
    ///Instances of this class can be used
    ///in order to complete the operation
    ///that checks whether a gamemasters
    ///password is valid during the
    ///authentication proccess.
    ///</summary>

    public class CheckGamemastersPassDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2 - 50 characters")]
        [Required(ErrorMessage = "This field is required")]
        public string? Username { get; set; }


        [StringLength(32, ErrorMessage = "password should not exceed 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$",
         ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character")]
        [Required(ErrorMessage = "This field is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? ConfirmedPassword { get; set; }
    }
}
