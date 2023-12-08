using QuizballApp.Data;
using QuizballApp.DTO.ParticipantsDTO;

namespace QuizballApp.Services
{
    public interface IParticipantService
    {
        Task<ParticipantReadOnlyDTO> CreateParticipantAsync(CreateParticipantDTO dto);
        Task<ParticipantReadOnlyDTO> ChangeParticipantsNameAsync(int participantId, string name);
        Task<ParticipantReadOnlyDTO> DeleteParticipantAsync(int id);
        Task<List<ParticipantReadOnlyDTO>> GetParticipantsByTypeAsync(int gamemasterId, string type);
   
        Task<bool> CheckParticipantsNameAsync(int? gamemasterId, string name);
    }
}
