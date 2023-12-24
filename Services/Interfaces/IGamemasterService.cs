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

        /// <summary>
        /// Runs asychronously and is used for deleting a gamemaster.
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <returns>(Gamemaster)The gamemaster that was deleted</returns>
        Task<Gamemaster?> DeleteGamemasterAsync(int id);
     
        /// <summary>
        /// Runs asychronously and is used for checking whether a username is
        /// available during the creation of a new gamemaster or during the 
        /// update of an existing one.
        /// </summary>
        /// <param name="gamemasterId">(int)the id of the gamemaster</param>
        /// <param name="username">(string)the username whose availability
        ///                         is going to be checked</param>
        /// <returns></returns>
        Task<bool> IsUsernameAvailable(int gamemasterId, string username);

        /// <summary>
        /// Runs asychronously and is used for checking whether an email is
        /// avaible during the creation of a new gamemaster or during the 
        /// update of an existing one.
        /// </summary>
        /// <param name="dto">(CheckEmailDTO)Contains all the nessecary data
        ///                     needed for this operation</param>
        /// <returns></returns>
        Task<bool> IsEmailAvailable(CheckEmailDTO dto);

        /// <summary>
        /// Runs asychronously and returns a specific gamemaster based on
        /// the provided id.
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <returns>(GamemasterReadOnlyDTO) The gamemaster that was found</returns>
        Task<GamemasterReadOnlyDTO?> GetGamemasterAsync(int id);

        /// <summary>
        /// Runs asychronously and is used to create a JWT token for a
        /// specific gamemaster.
        /// </summary>
        /// <param name="gmId">(int) the id of the gamemaster</param>
        /// <param name="gmName">(string) the username of the gamemaster</param>
        /// <param name="appSecurityKey">(string) the app's security key</param>
        /// <returns>(string) the JWT token that is generated</returns>
        string CreateGamemasterToken(int gmId, string gmName, string appSecurityKey);

        /// <summary>
        /// Runs asychronously and is used to update an existing gamemaster.
        /// </summary>
        /// <param name="dto">(UpdateGamemasterDTO) Contains all the nessecary data
        ///                     needed for this operation </param>
        /// <returns>(UpdateGamemasterReadOnlyDTO) the gamemaster that was updated</returns>
        Task<UpdateGamemasterReadOnlyDTO> UpdateGamemasterAsync(UpdateGamemasterDTO dto);

        /// <summary>
        /// Runs asychronously and is used in order to change the password
        /// of an existing gamemaster.
        /// </summary>
        /// <param name="dto">(ChangeGamemasterPassDTO) Contains all the nessecary data
        ///                     needed for this operation </param>
        /// <returns>(bool) True if the operation was completed successfull and false if not</returns>
        Task<bool> ChangeGamemasterPassAsync(ChangeGamemasterPassDTO dto);
    }
}
