using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GamemasterDTO
{
    public class CheckEmailDTO : BaseDTO
    {
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email adress")]
        public string? Email { get; set; }
    }
}
