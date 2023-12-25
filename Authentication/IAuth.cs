using QuizballApp.DTO.GamemasterDTO;

namespace QuizballApp.Authentication
{
    ///<summary>
    ///This interface contains a method that
    ///performs Authentication logic.
    ///</summary>
    public interface IAuth
    {

        /// <summary>
        /// Runs asychronously and checks whether 
        /// a gamemasters credentials are valid.
        /// </summary>
        /// <param name="dto">(LoginDTO) Contains the data needed
        ///                                to execute this operation</param>
        /// <returns>(AuthenticationDTO) Contains the data of the gamemaster
        ///                             and the JWT token</returns>
        Task<AuthenticationDTO> CheckGamemasterAsync(LoginDTO dto);
    }
}
