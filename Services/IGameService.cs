using QuizballApp.Data;
using QuizballApp.DTO;

namespace QuizballApp.Services
{
    public interface IGameService
    {
        Task<Game> CreateGameAsync(CreateGameDTO dto);
        Task<Game> UpdateGameAsync(GamesEndDTO dto);
        Task<IList<Game>> GetGameByParticipantsAsync(GetGameByParticipantsDTO dto);
        Task<bool> AddQuestionAsync(int gameId, Question question);
    }
}
