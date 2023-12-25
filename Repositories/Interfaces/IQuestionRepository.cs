using QuizballApp.Data;

namespace QuizBall.Repositories
{

    ///<summary>
    ///This interface contains the extra methods needed by the question entity related operations
    ///apart from those that are inherited by the base repository.
    ///</summary>

    public interface IQuestionRepository
    {
        /// <summary>
        /// Runs asychronoysly and returns a random question of a specific category and
        /// a specific difficult level.
        /// </summary>
        /// <param name="gamemasterId">(int) It is always null. Because these questions are not 
        ///                             related to any gamemaster</param>
        /// <param name="categoryId">(int)The category id</param>
        /// <param name="difficultyId">(int) The difficulty id</param>
        /// <param name="lastQuestion">(int) The id of the last question that was asked
        ///                             based on these criteria so that the same
        ///                             question will not be returned more than once
        ///                             during a game</param>
        /// <returns>(Question)A random question that meet the provided criteria</returns>
        Task<Question> GetRandomQuestionAsync(int? gamemasterId, int categoryId, int difficultyId, int lastQuestion);
        
        /// <summary>
        /// Runs asychronously and returns all the custom questions of 
        /// a gamemaster.
        /// </summary>
        /// <param name="gamemasterId">(int) The gamemasters id</param>
        /// <returns>(IEnumerable<Questions>) All the questiones that meet the provided
        ///                                    criteria</returns>
        Task<IEnumerable<Question>> GetCustomQuestionsAsync(int gamemasterId);

        /// <summary>
        /// It is not used and will be deleted!
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        Task<bool> AddGameToQuestion(int questionId, Game game);

        /// <summary>
        /// Retu rns all the customs questions of a gamemaster filtered by category.
        /// </summary>
        /// <param name="catId">(int)The categories id</param>
        /// <param name="gamemaster">(int)The gamemasters id</param>
        /// <returns></returns>
        Task<IEnumerable<Question>> GetCustQuestionsByCatAsync(int catId, int gamemaster);
    }
}
