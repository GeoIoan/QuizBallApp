using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO
{
    public class UpdateQuestionDTO : BaseDTO
    {
        [StringLength(250, ErrorMessage = "The question should not exceed 250 characters"),
         Required(ErrorMessage = "This field is required")]
        public string? Question { get; set; }
        [Url(ErrorMessage = "Please provide a valid url")]
        public string? Media { get; set; }
        public int GamemasterId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int DifficultyId { get; set; }

        [StringLength(250, ErrorMessage = "The answer should not exceed 250 characters"),
        Required(ErrorMessage = "This field is required")]
        public string? Answers { get; set; }
    }
}
