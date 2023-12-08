using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<Participant>> GetParticipantsByTypeAsync(int gamemasterId, string participantType);
        Task<Participant?> ChangeParticipantsName(int participantId, string newName);

        Task<bool> CheckParticipantsName(int? gamemasterid,  string participantsName);
    }
}
