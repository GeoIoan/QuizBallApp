using QuizballApp.Data;
using QuizballApp.DTO.GamemasterDTO;

namespace QuizBall.Repositories
{
    public interface IGamemasterRepository
    {
        Task<Gamemaster?> CheckGamemasterAsync(string username, string password);
     

        Task<bool>CheckUsernameAsync(int gamemasterId, string username);

        Task<bool> CheckEmailAsync(CheckEmailDTO dto);

        Task<UpdateGamemasterReadOnlyDTO> UpdateGamemasterAsync(UpdateGamemasterDTO dto);

        Task<bool> ChangeGamemasterPasswordAsync(ChangeGamemasterPassDTO dto);
    }
}
