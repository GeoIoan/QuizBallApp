using QuizballApp.Data;

namespace QuizballApp.DTO.GameDTO
{
    public class AddQuestionsDTO
    {
        public List<Question> Questions { get; set; }
        public int GameId { get; set; }
    }
}
