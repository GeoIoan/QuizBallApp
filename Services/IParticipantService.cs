using QuizballApp.Data;
using QuizballApp.DTO;

namespace QuizballApp.Services
{
    public interface IParticipantService
    {
        Task<Participant> CreateParticipantAsync(CreateParticipantDTO dto);
        Task<Participant> ChangeParticipantsNameAsync(int participantId, string name);
        Task<Participant> DeleteParticipantAsync(int id);
        Task<List<Participant>> GetParticipantsByTypeAsync(int gamemasterId, string type);
    }
}
