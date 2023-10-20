using QuizballApp.Data;
using QuizballApp.DTO;

namespace QuizballApp.Services
{
    public interface IGamemasterService
    {
        Task<Gamemaster?> SingUpAsync(CreateGamemasterDTO dto);
        Task<Gamemaster?> UpdateGamemasterAsync(UpdateGamemasterDTO dto);
        Task<Gamemaster?> DeleteGamemasterAsync(int id);
        Task<Gamemaster?> GetGamemasterByUsernameAsync(string username);
    }
}
