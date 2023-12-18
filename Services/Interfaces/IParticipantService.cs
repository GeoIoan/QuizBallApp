using QuizballApp.Data;
using QuizballApp.DTO.ParticipantsDTO;
///<summary>
///This interface contains all the methods that are needed
///so that the bussiness logic of the participant entity can be
///implemented.
///</summary>

namespace QuizballApp.Services
{
    public interface IParticipantService
    {
        /// <summary>
        /// Runs asychronously and is used to create a new participant.
        /// </summary>
        /// <param name="dto">(CreateParticipantDTO) Contains the data needed for this operation</param>
        /// <returns>(ParticipantReadOnlyDTO)The participant that was added</returns>
        Task<ParticipantReadOnlyDTO> CreateParticipantAsync(CreateParticipantDTO dto);
        
        /// <summary>
        /// Runs asychronously and is used to change a specific participants name.
        /// </summary>
        /// <param name="participantId">(int) The participants id</param>
        /// <param name="name">(string) The new name</param>
        /// <returns>(ParticipantReadOnlyDTO) The updated participant</returns>
        Task<ParticipantReadOnlyDTO> ChangeParticipantsNameAsync(int participantId, string name);
       
        /// <summary>
        /// Runs asychronously and deletes a specific participant.
        /// </summary>
        /// <param name="id">(int) The participants id</param>
        /// <returns>(ParticipantReadOnlyDTO) The deleted participant</returns>
        Task<ParticipantReadOnlyDTO> DeleteParticipantAsync(int id);
        
        /// <summary>
        /// Runs asychronously and returns all the participants of specific gamemaster
        /// filtered by their type.
        /// </summary>
        /// <param name="gamemasterId">(int) The gamemasters id</param>
        /// <param name="type">(string) The type of the participants</param>
        /// <returns>(List<ParticipantReadOnlyDTO>) Returns all the participants that meet the provided criteria</returns>
        Task<List<ParticipantReadOnlyDTO>> GetParticipantsByTypeAsync(int gamemasterId, string type);
   
        /// <summary>
        /// Runs asychronously and check the availability of a participants name when a new
        /// participant is created or an existing one is updated.
        /// </summary>
        /// <param name="gamemasterId">(int)The gamemasters id</param>
        /// <param name="name">(string)The new name</param>
        /// <returns>(bool)True if the name is available false if not</returns>
        Task<bool> CheckParticipantsNameAsync(int? gamemasterId, string name);
    }
}
