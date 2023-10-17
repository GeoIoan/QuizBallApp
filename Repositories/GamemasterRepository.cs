using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;
using UsersApp.Security;

namespace QuizBall.Repositories
{
    public class GamemasterRepository : BaseRepository<Gamemaster>, IGamemasterRepository
    {
        public GamemasterRepository(QuizballDbContext context) : base(context)
        {
        }

        public async Task<Gamemaster?> CheckGamemasterAsync(string username, string password)
        {
            var gamemaster = await _context.Gamemasters.FirstOrDefaultAsync(g => g.Username == username);
            if (gamemaster is null) return null;

            if (!EncryptionUtil.IsValidPassword(password, gamemaster.Password!)) return null;

            return gamemaster;
        }

        public async Task<Gamemaster?> GetByUsernameAsync(string username)
        {
            return await _context.Gamemasters.Where(g => g.Username == username).FirstOrDefaultAsync();
        }
    }
}
