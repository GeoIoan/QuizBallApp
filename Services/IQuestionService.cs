using QuizballApp.Data;
using QuizballApp.DTO;

namespace QuizballApp.Services
{
    public interface IQuestionService
    {
        Task<Question> CreateCustomQuestionAsync(CreateQuestionDTO dto);
        Task<Question> UpdateCustomQuestionAsync(UpdateQuestionDTO dto);
        Task<Question> DeleteCustomQuestionAsync(int id);
        Task<IList<Question>> GetCustomQuestionsAsync(int gamemasterId);
        Task<Question> GetRandomQuestionAsync(SelectQuestionDTO dto);
        Task<bool> AddGameAsync(int questionId, Game game);
        Task<IList<Question>> GetCustQuestionsByCatAsync(int gamemasterId, int catId);
    }
}
