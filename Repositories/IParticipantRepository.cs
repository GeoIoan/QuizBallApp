

using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<Participant>> GetParticipantsByTypeAsync(string participantType);
    }
}
