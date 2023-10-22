using QuizballApp.Data;
using QuizballApp.DTO;

namespace QuizballApp.Services
{
    public interface IGameService
    {
        Task<GameReadOnlyDTO> CreateGameAsync(CreateGameDTO dto);
        Task<GameReadOnlyDTO> UpdateGameAsync(GamesEndDTO dto);
        Task<IList<GameReadOnlyDTO>> GetGameByParticipantsAsync(GetGameByParticipantsDTO dto);
        Task<bool> AddQuestionAsync(int gameId, Question question);
    }
}
