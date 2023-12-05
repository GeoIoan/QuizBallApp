using QuizballApp.Data;
using QuizballApp.DTO.GameDTO;

namespace QuizBall.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGamesByGamemasterAsync(int gamemasterId);
        Task<IEnumerable<Game>> GetGameByParticipantsAsync(int gamemasterId, int participantId1, int participantId2);

        Task<bool> AddQuestionsToGame(int gameId, List<Question> questions);

        Task<Game?> GamesEndUpdate(GamesEndDTO dto);
        
    }
}
