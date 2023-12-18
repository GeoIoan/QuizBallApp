using QuizballApp.Data;
///<summary>
///This interface contains the extra methods needed by the participant entity related operations
///apart from those that are inherited by the base repository.
///<summary>

namespace QuizBall.Repositories
{
    public interface IParticipantRepository
    {
        /// <summary>
        /// Runs asychronally and returns all the player or the team(depending on the users choice)
        /// registries that are related to a specific gamemaster registry
        /// </summary>
        /// <param name="gamemasterId">(int) The gamemasters id</param>
        /// <param name="participantType">(string) the users choice of participants</param>
        /// <returns>(IEnumerable<Participant>)The participants found based on the 
        ///                                     provided criteria</returns>
        Task<IEnumerable<Participant>> GetParticipantsByTypeAsync(int gamemasterId, string participantType);
        
        /// <summary>
        /// Runs asychronously and changes the value of the name field
        /// of a particular participant registry.
        /// </summary>
        /// <param name="participantId">(int)The participants id</param>
        /// <param name="newName">(string) The new name</param>
        /// <returns>(Participant)The updated participant</returns>
        Task<Participant?> ChangeParticipantsName(int participantId, string newName);

        /// <summary>
        /// Runs asychronously and checks if specific name is available
        /// when the user tries to insert a new participant registry or
        /// update an existing one.
        /// </summary>
        /// <param name="gamemasterid">(int)The Gamemasters id</param>
        /// <param name="participantsName">(string)The name whose availability is checked</param>
        /// <returns>(bool) True if the name is avaialable false if not</returns>
        Task<bool> CheckParticipantsName(int? gamemasterid,  string participantsName);
    }
}
