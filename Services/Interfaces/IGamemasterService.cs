using QuizballApp.Data;
using QuizballApp.DTO.GamemasterDTO;
///<summary>
///This interface contains all the methods that are needed
///so that the bussiness logic of the question entity can be
///implemented.
///</summary>


namespace QuizballApp.Services
{
    public interface IGamemasterService
    {
        /// <summary>
        /// Runs asychonously and is used for singing up a new gamemaster.
        /// </summary>
        /// <param name="dto">(CreateGamemasterDTO)Contains all the data needed to perform this operation</param>
        /// <returns>(Gamemaster)The gamemaster that was just inserted</returns>
        Task<Gamemaster?> SingUpAsync(CreateGamemasterDTO dto);
        Task<Gamemaster?> DeleteGamemasterAsync(int id);
     
        Task<bool> IsUsernameAvailable(int gamemasterId, string username);

        Task<bool> IsEmailAvailable(CheckEmailDTO dto);

        Task<GamemasterReadOnlyDTO?> GetGamemasterAsync(int id);

        string CreateGamemasterToken(int gmId, string gmName, string appSecurityKey);

        Task<UpdateGamemasterReadOnlyDTO> UpdateGamemasterAsync(UpdateGamemasterDTO dto);

        Task<bool> ChangeGamemasterPassAsync(ChangeGamemasterPassDTO dto);
    }
}
