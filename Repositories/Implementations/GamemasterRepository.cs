using Humanizer;
using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;
using QuizballApp.DTO.GamemasterDTO;
using UsersApp.Security;

namespace QuizBall.Repositories
{
    public class GamemasterRepository : BaseRepository<Gamemaster>, IGamemasterRepository
    {
        public GamemasterRepository(QuizballDbContext context) : base(context)
        {
        }

        public async Task<bool> ChangeGamemasterPasswordAsync(ChangeGamemasterPassDTO dto)
        {
            var gm = await _context.Gamemasters.Where(g => g.Id == dto.Id).FirstOrDefaultAsync();
            if (gm is null) return false;

            gm.Password = EncryptionUtil.Encrypt(dto.Password!);

            _context.Gamemasters.Update(gm);

            return true;
        }

        public async Task<bool> CheckEmailAsync(CheckEmailDTO dto)
        {
            if(dto.Id == 0)
            {
                var gm = await _context.Gamemasters.Where(g => g.Email == dto.Email).FirstOrDefaultAsync();
                if (gm is null) return true;
                return false;
            }
            else
            {
                var gm = await _context.Gamemasters.Where(g => g.Id != dto.Id && g.Email == dto.Email).FirstOrDefaultAsync();
                if (gm is null) return true;
                return false;
            }
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
            if(gamemasterId == 0)
            {
                var gm = await _context.Gamemasters.Where(g => g.Username == username).FirstOrDefaultAsync();
                if (gm is null) return true;
                return false;
            }
            else
            {
                var gm = await _context.Gamemasters.Where(g => g.Id != gamemasterId && g.Username == username).FirstOrDefaultAsync();
                if (gm is null) return true;
                return false;
            }
        }

        public async Task<UpdateGamemasterReadOnlyDTO> UpdateGamemasterAsync(UpdateGamemasterDTO dto)
        {
           var gm = await _context.Gamemasters.Where(g => g.Id == dto.Id).FirstOrDefaultAsync();
            if (gm is null) return null;

            gm.Username = dto.Username;
            gm.Email = dto.Email;

            _context.Gamemasters.Update(gm);

            var readOnlyGm = new UpdateGamemasterReadOnlyDTO()
            {
                Id = gm.Id,
                Username = gm.Username,
                Email = gm.Email
            };

           
            return readOnlyGm;
        }



    }
}
