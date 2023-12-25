using System.ComponentModel.DataAnnotations;
namespace QuizballApp.DTO
{

    ///<summary>
    ///Instances of this class
    ///transfer the data needed to
    ///complete the operation of 
    ///creating a new question.
    ///</summary>
    public class CreateQuestionDTO
    {
        [StringLength(250, ErrorMessage= "The question should not exceed 250 characters"), 
         Required(ErrorMessage= "This field is required")]
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

        [StringLength(250, ErrorMessage = "The answer should not exceed 250 characters"),
      Required(ErrorMessage = "This field is required")]
        public string? FiftyFifty { get; set; }
    }
}
