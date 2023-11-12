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

        public async Task<bool> CheckUsernameAsync(int gamemasterId, string username)
        {
            var gm = await _context.Gamemasters.Where(g => g.Username == username).FirstOrDefaultAsync();
            if ( gm is null) return true;
            else
            {
                if (gamemasterId == 0) return false;
                else
                {
                    if (gamemasterId == gm.Id) return true;
                    else return false;
                }
            }
        }
    }
}
