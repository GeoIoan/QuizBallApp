using QuizballApp.Data;
using QuizballApp.DTO;
using QuizballApp.DTO.QuestionDTO;

namespace QuizballApp.Services
{
    public interface IQuestionService
    {
        Task<QuestionReadOnlyDTO> CreateCustomQuestionAsync(CreateQuestionDTO dto);
        Task<QuestionReadOnlyDTO> UpdateCustomQuestionAsync(UpdateQuestionDTO dto);
        Task<QuestionReadOnlyDTO> DeleteCustomQuestionAsync(int id);
        Task<IList<QuestionReadOnlyDTO>> GetCustomQuestionsAsync(int gamemasterId);
        Task<QuestionReadOnlyDTO> GetRandomQuestionAsync(SelectQuestionDTO dto);
        Task<bool> AddGameAsync(int questionId, InsertGameToQuestionDTO dto);
        Task<IList<QuestionReadOnlyDTO>> GetCustQuestionsByCatAsync(int gamemasterId, int catId);
    }
}
