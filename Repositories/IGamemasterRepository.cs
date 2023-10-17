
using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public interface IGamemasterRepository
    {
        Task<Gamemaster?> CheckGamemasterAsync(string username, string password);
        Task<Gamemaster?> GetByUsernameAsync(string username);
    }
}
