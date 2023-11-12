using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public interface IGamemasterRepository
    {
        Task<Gamemaster?> CheckGamemasterAsync(string username, string password);
     

        Task<bool>CheckUsernameAsync(int gamemasterId, string username);
    }
}
