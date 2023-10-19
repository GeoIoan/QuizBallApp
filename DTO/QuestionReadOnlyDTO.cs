using System.ComponentModel.DataAnnotations;

namespace QuizballApp.DTO
{
    public class QuestionReadOnlyDTO
    {
       
        public string? Question { get; set; }
       
        public string? Media { get; set; }
     
        public string? CategoryName { get; set; }

        public string? DifficultyLevel { get; set; }

        public string? Answers { get; set; }
    }
}
