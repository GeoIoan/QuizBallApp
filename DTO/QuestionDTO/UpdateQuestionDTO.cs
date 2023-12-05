using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO
{
    public class UpdateQuestionDTO : BaseDTO
    {
        [StringLength(250, ErrorMessage = "The question should not exceed 250 characters"),
         Required(ErrorMessage = "This field is required")]
        public string? Question1 { get; set; }
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

        [StringLength(250, ErrorMessage = "Fifty fifty should not exceed 150 characters"),
        Required(ErrorMessage = "This field is required")]
        public string? FiftyFifty { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UpdateQuestionDTO dTO &&
                   Id == dTO.Id &&
                   Question1 == dTO.Question1 &&
                   Media == dTO.Media &&
                   GamemasterId == dTO.GamemasterId &&
                   CategoryId == dTO.CategoryId &&
                   DifficultyId == dTO.DifficultyId &&
                   Answers == dTO.Answers;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Question1, Media, GamemasterId, CategoryId, DifficultyId, Answers);
        }
    }
}
