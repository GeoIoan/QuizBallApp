using System.ComponentModel.DataAnnotations;

namespace QuizballApp.DTO
{
    public class CreateQuestionDTO
    {
        [StringLength(250, ErrorMessage= "The question should not exceed 250 characters"), 
         Required(ErrorMessage= "This field is required")]
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
