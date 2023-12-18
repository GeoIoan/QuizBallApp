using QuizballApp.Data;
using QuizballApp.DTO.GamemasterDTO;

///<summary>
///This interface contains the extra methods needed by the gamemaster entity
///apart from those that are inherited by the base repository.
///<summary>

namespace QuizBall.Repositories
{
    public interface IGamemasterRepository
    {
        /// <summary>
        /// Runs asychronally and checks whether the credentials of a gamemaster
        /// are legitimate.
        /// </summary>
        /// <param name="username">(string)The username of the gamemaster</param>
        /// <param name="password">(password)The password of the gamemaster</param>
        /// <returns>(Gamemaster!)The gamemastes registry if the credentials are valid 
        ///                        and null if they are invalid</returns>
        Task<Gamemaster?> CheckGamemasterAsync(string username, string password);
     
        /// <summary>
        /// Runs asychronally and checks whether a gamemasters name is available
        /// or not when a user tries to create a new gamemaster registry or update an existing one.
        /// </summary>
        /// <param name="gamemasterId">(int)the id of the gamemaster to be updated, 
        ///                             if the user tries to create a gamemaster
        ///                             then the id is always 0</param>
        /// <param name="username">(string) the username whose availability is going to be checked</param>
        /// <returns>(bool) True if the username is avaialable false if not</returns>
        Task<bool>CheckUsernameAsync(int gamemasterId, string username);

        /// <summary>
        /// Runs asychronally and check whether an email is available or not during
        /// the creation of the update a gamemaste registry.
        /// </summary>
        /// <param name="dto">(CheckEmailDTO)Contains all the nessecary data for the forementioned check.</param>
        /// <returns>(bool) True if the email is available, false if not</returns>
        Task<bool> CheckEmailAsync(CheckEmailDTO dto);

        /// <summary>
        /// Runs asychronally and updates a gamemaster registry.
        /// </summary>
        /// <param name="dto">(UpdateGamemsterDTO)Contains all the nessecary data for the update operation.</param>
        /// <returns>(UpdateGamemasterReadOnlyDTO?)The updated data if the operation was successfull
        ///                                         and null if not</returns>
        Task<UpdateGamemasterReadOnlyDTO?> UpdateGamemasterAsync(UpdateGamemasterDTO dto);

        /// <summary>
        /// Runs asychonally and changes the password on a gamemaster registry.
        /// </summary>
        /// <param name="dto">(ChangeGamemasterPassDTO)Contains all the nessecary data for the forementioned operation.</param>
        /// <returns>(bool)True if the operation was successfull and false if not.</returns>
        Task<bool> ChangeGamemasterPasswordAsync(ChangeGamemasterPassDTO dto);
    }
}
