

using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> GetRandomQuestionAsync(int gamemasterId, int categoryId, int difficultyId);
        Task<IEnumerable<Question>> GetCustomQuestionsAsync(int gamemasterId); 
    }
}
