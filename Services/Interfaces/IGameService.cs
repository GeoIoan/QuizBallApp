using QuizballApp.Data;
using QuizballApp.DTO.GameDTO;

namespace QuizballApp.Services
{

    ///<summary>
    ///This interface contains all the methods that are needed
    ///so that the bussiness logic of the participant entity can be
    ///implemented.
    ///</summary>

    public interface IGameService
    {
        /// <summary>
        /// Runs asychronously and is used for a creating a game registry.
        /// </summary>
        /// <param name="dto">(CreateGameDTO) All the data needed for this operation</param>
        /// <returns>(GameReadOnlyDTO) The game that was created</returns>
        Task<GameReadOnlyDTO> CreateGameAsync(CreateGameDTO dto);
       
        /// <summary>
        /// It is not used, will be deleted
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<GameReadOnlyDTO> UpdateGameAsync(GamesEndDTO dto);

        /// <summary>
        /// Runs asychronously and is used for geting the games that are related with
        /// two specific participants.
        /// </summary>
        /// <param name="dto">(GameReadOnlyDTO)Contains all the data needed for this operation</param>
        /// <returns>(GameReadOnlyDTO)The game registry that was updated</returns>
        Task<IList<GameReadOnlyDTO>> GetGameByParticipantsAsync(GetGameByParticipantsDTO dto);
        
        /// <summary>
        /// Runs asychronously and is used to relate a game resitry with
        /// question registries
        /// </summary>
        /// <param name="gameId">(int)The id of the game registry</param>
        /// <param name="questions">(List<Question>) The questions.</param>
        /// <returns>(bool) True if the operation was completed successfully, false if not</returns>
        Task<bool> AddQuestionsAsync(int gameId, List<Question> questions);

        /// <summary>
        /// Runs asychronously and is used to relate the game registry with the
        /// two participant registries which represent the participants of a game.
        /// </summary>
        /// <param name="gameId">(int) the id of the game</param>
        /// <param name="participant1">(Participant)participant 1</param>
        /// <param name="participant2">(Participant)participant 2</param>
        /// <returns>(bool) True if the operation was completed successfully, false if not</returns>
        Task<bool> AddParticipantsAsync(int gameId, Participant participant1, Participant participant2);
    }
}
