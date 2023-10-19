using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(QuizballDbContext context) : base(context) { }

        public async Task<IEnumerable<Game>> GetAllGamesByGamemasterAsync(int gamemasterId)
        {
            var games = await _context.Games.Where(g => g.GamemasterId == gamemasterId).ToListAsync();
            return games;       
        }

        public async Task<IEnumerable<Game>> GetGameByParticipantsAsync(int gamemasterId, int participantId1, int participantId2)
        {
            var games = await _context.Games.Where(g => (g.GamemasterId == gamemasterId) && 
                                                         (g.Participant1Id == participantId1 
                                                         || g.Participant1Id == participantId2)
                                                         && (g.Participant2Id == participantId1 
                                                         || g.Participant2Id == participantId2))
                                                         .ToListAsync();

            return games;
        }

        public async Task AddQuestionToGame(int gameId, Question question)
        {

            var game = await _context.Games
                .Include(g => g.Questions)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            game!.Questions.Add(question);


            await _context.SaveChangesAsync();
        }
    }
}

