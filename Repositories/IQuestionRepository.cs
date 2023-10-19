

using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> GetRandomQuestionAsync(int gamemasterId, int categoryId, int difficultyId);
        Task<IEnumerable<Question>> GetCustomQuestionsAsync(int gamemasterId);

        Task AddGameToQuestion(int questionId, Game game);

        Task<IEnumerable<Question>> GetCustQuestionsByCatAsync(int catId, int gamemaster);
    }
}
