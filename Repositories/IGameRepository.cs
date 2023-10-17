

using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGamesByGamemasterAsync(int gamemasterId);
        Task<IEnumerable<Game>> GetGameByParticipantsAsync(int gamemasterId, int participantId1, int participantId2);
    }
}
