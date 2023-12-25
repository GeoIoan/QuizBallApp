using QuizballApp.Data;

namespace QuizballApp.DTO.GameDTO
{
    ///<summary>
    ///Instances of this class are used
    ///to transfer the needed to complete
    ///the operation that creates a relation
    ///between a specific games and and the 
    ///questions that were asked during
    ///its duration.
    ///</summary>>
    public class AddQuestionsDTO
    {
        public List<Question> Questions { get; set; }
        public int GameId { get; set; }
    }
}
