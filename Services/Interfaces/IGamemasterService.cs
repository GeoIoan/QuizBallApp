using QuizballApp.Data;
using QuizballApp.DTO.GamemasterDTO;

namespace QuizballApp.Services
{
    public interface IGamemasterService
    {
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
