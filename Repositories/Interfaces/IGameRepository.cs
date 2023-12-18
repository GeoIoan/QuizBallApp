using QuizballApp.Data;
using QuizballApp.DTO.GameDTO;
///<summary>
///This interface contains the extra methods needed by the game entity related operations
///apart from those that are inherited by the base repository.
///<summary>
namespace QuizBall.Repositories
{
    public interface IGameRepository
    {
        /// <summary>
        /// Runs asychronally and returns all the registries that are related
        /// to a specific gamemaster.
        /// </summary>
        /// <param name="gamemasterId">(int)The gamemaster's id</param>
        /// <returns>(IEnumerable<Game>) The games that are found</returns>
        Task<IEnumerable<Game>> GetAllGamesByGamemasterAsync(int gamemasterId);
       
        /// <summary>
        /// Runs asychronally and returns the game registries related to a 
        /// specific gamemaster registry that are also related with two participant registries
        /// </summary>
        /// <param name="gamemasterId">(int) The id of the gamemaster</param>
        /// <param name="participantId1">(int) The id of the participant 1</param>
        /// <param name="participantId2">(int) The id of participant 2</param>
        /// <returns>(IEnumerable<Game>) All the games found based on the provided criteria</returns>
        Task<IEnumerable<Game>> GetGameByParticipantsAsync(int gamemasterId, int participantId1, int participantId2);

        /// <summary>
        /// Runs asychronally and creates a relation between 2 specific participant registries
        /// and a game one. Is is required in order to create a game registry after the game is over
        /// on the client's side.
        /// </summary>
        /// <param name="gameId">(int) The id of the game</param>
        /// <param name="participant1">(int) The id of participant 1</param>
        /// <param name="participant2">(int) The id of participant 2</param>
        /// <returns>(bool) True if the operation was successfull and false if not</returns>
        Task<bool> AddParticipantsToGame(int gameId, Participant participant1, Participant participant2);

        /// <summary>
        /// Runs asychronally and creates a relation between 18 specific question registries
        /// and a game one. Is is required in order to create a game registry after the game is over
        /// on the client's side.
        /// </summary>
        /// <param name="gameId">(int) The id of the game</param>
        /// <param name="questions">(List<Question>)The questions the game is going to be related with</param>
        /// <returns>(bool) True if the operation was successfull and false if not</returns>
        Task<bool> AddQuestionsToGame(int gameId, List<Question> questions);

        /// <summary>
        /// It is not used, will be deleted!
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<Game?> GamesEndUpdate(GamesEndDTO dto);
        
    }
}
